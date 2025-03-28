import { Component, computed, signal, WritableSignal } from '@angular/core';
import { BonusesComponent } from "@app/bonuses/bonuses/bonuses.component";
import { Fieldset } from "primeng/fieldset";
import { InputText } from "primeng/inputtext";
import { NgIf } from "@angular/common";
import { FormGroup, ReactiveFormsModule } from "@angular/forms";
import { Textarea } from "primeng/textarea";
import { Campaign } from '@app/campaigns/models/campaign';
import { Archetype } from '@app/models/archetypes/archetype';
import { EditorAction, EntityActionData } from '@app/models/EntityActionData';
import {DialogService, DynamicDialogConfig, DynamicDialogRef} from 'primeng/dynamicdialog';
import { ArchetypesService } from '@services/archetypes/archetypes.service';
import { v4 as uuid } from 'uuid';
import { getAsForm } from '@app/tokens/EditorExtension';
import { canEditCampaign, canEditTemplate } from '@app/tokens/utils.funcs';
import { Bonus } from '@app/models/bonuses/bonus';
import { firstValueFrom, Observable, of } from 'rxjs';
import { Encounter } from '@app/encounters/models/encounter';
import { EncountersService } from '@app/encounters/services/encounters.service';
import { Creature } from '@app/campaigns/models/creature';
import { CreatureCategory } from '@app/campaigns/models/CreatureCategory';
import { CreatureSelectTableComponent } from '@app/creatures/creature-select-table/creature-select-table.component';
import { GridComponent, RRColumns, RRHeaderAction } from '@app/components/grid/grid.component';
import { GetListInput } from '@app/tokens/get-list-input';
import { ButtonDirective } from 'primeng/button';
import { IftaLabel, IftaLabelModule } from 'primeng/iftalabel';
import { PagedOutput } from '@app/models/PagedOutput';

@Component({
  selector: 'rr-encounter-editor',
  imports: [
    Fieldset,
    InputText,
    NgIf,
    ReactiveFormsModule,
    GridComponent,
    ButtonDirective,
    IftaLabelModule
  ],
  templateUrl: './encounter-editor.component.html',
  styleUrl: './encounter-editor.component.scss'
})
export class EncounterEditorComponent {
  public loaded = signal(false);
  headerActions: RRHeaderAction[] = [];
  columns: RRColumns[] = [];
  refreshGrid = signal<boolean>(false);
  public form: FormGroup;
  public campaign: Campaign;
  public encounter: WritableSignal<Encounter>;
  public creatures = computed(() => this.encounter().creatures);
  private action: EditorAction = EditorAction.create;


  constructor(dialogConfig: DynamicDialogConfig,
              private service: EncountersService,
              private dialogRef: DynamicDialogRef<EncounterEditorComponent>,
              private dialog: DialogService) {
    this.campaign = dialogConfig.data.campaign as Campaign;
    const encounter = dialogConfig.data.encounter as Encounter;

    if (encounter) {
      this.action = EditorAction.update;
    } else {
      this.action = EditorAction.create;
    }

    this.encounter = signal(encounter ?? {
      id: uuid(),
      name: '',
      creatures: [],
    });

    this.form = getAsForm(this.encounter(), false, ['name']);
    if (!canEditCampaign(this.campaign)) {
      this.form.disable();
    }
    this.headerActions = this.buildHeaderActions();
    this.columns = this.buildColumns();
    this.loaded.set(true);
  }
  public selectCreatureTemplate() {
    this.dialog.open(CreatureSelectTableComponent, {
      data: { creatureCategory: CreatureCategory.Enemy, campaign: this.campaign },
      height: '90vh',
      width: '80vh',
      closable: true
    }).onClose.subscribe(async (creature: Creature) => {
      if (creature) {
        await this.addCreature(creature);
      }
    })
  }
  getList = (input: GetListInput) => {
    if (this.action === EditorAction.create) {
      return of<PagedOutput<Creature>>({
        items: this.creatures(),
        totalCount: this.creatures().length
      });
    } else {
      return this.service.getAllCreatures(this.campaign.id, this.encounter().id, input);
    }
  }
  async addCreature(creature: Creature): Promise<void> {
    if (this.action === EditorAction.create) {
      this.encounter.set({
        ...this.encounter(),
        creatures: [...this.encounter().creatures, creature]
      });
    } else {
      await firstValueFrom(this.service.addCreature(this.campaign.id, this.encounter().id, creature));
    }
      this.refreshGrid.set(true);
  }
  async removeCreature(creatureId: string) {
      await firstValueFrom(this.service.removeCreature(this.campaign.id, this.encounter().id, creatureId));
    this.encounter.set(await firstValueFrom(this.service.getById(this.campaign.id, this.encounter().id)));
  }
  private buildHeaderActions() {
    return [
      {
        icon: 'pi pi-plus',
        condition: () => true,
        tooltip: 'Add',
        callBack: () => this.selectCreatureTemplate(),
      } as RRHeaderAction
    ];
  }

  private buildColumns() {
    return [{
      header: 'Name',
      property: 'name'
    } as RRColumns];
  }

  public async save(): Promise<void> {
    if (!this.form.valid) {
      return;
    }
    const encounter = this.form.value as Encounter;
    const saveAction = this.action === EditorAction.create
      ? this.service.create(this.campaign.id, encounter)
      : this.service.update(this.campaign.id, encounter);

    await firstValueFrom(saveAction);
    this.dialogRef.close(true);
  }
}

