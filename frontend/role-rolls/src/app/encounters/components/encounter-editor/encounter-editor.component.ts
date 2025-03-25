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
import { DialogService, DynamicDialogConfig } from 'primeng/dynamicdialog';
import { ArchetypesService } from '@services/archetypes/archetypes.service';
import { v4 as uuid } from 'uuid';
import { getAsForm } from '@app/tokens/EditorExtension';
import { canEdit } from '@app/tokens/utils.funcs';
import { Bonus } from '@app/models/bonuses/bonus';
import { firstValueFrom, Observable } from 'rxjs';
import { Encounter } from '@app/encounters/models/encounter';
import { EncountersService } from '@app/encounters/services/encounters.service';
import { Creature } from '@app/campaigns/models/creature';
import { CreatureCategory } from '@app/campaigns/models/CreatureCategory';
import { CreatureSelectTableComponent } from '@app/creatures/creature-select-table/creature-select-table.component';
import { GridComponent, RRColumns, RRHeaderAction } from '@app/components/grid/grid.component';
import { GetListInput } from '@app/tokens/get-list-input';

@Component({
  selector: 'rr-encounter-editor',
  imports: [
    Fieldset,
    InputText,
    NgIf,
    ReactiveFormsModule,
    GridComponent
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

    this.form = getAsForm(this.encounter());
    if (!canEdit(this.campaign)) {
      this.form.disable();
    }
    this.headerActions = this.buildHeaderActions();
    this.columns = this.buildColumns();
    this.loaded.set(true);
  }
  public selectCreatureTemplate() {
    this.dialog.open(CreatureSelectTableComponent, {
      data: { creatureCategory: CreatureCategory.Enemy, campaign: this.campaign }
    }).onClose.subscribe(async (creature: Creature) => {
      if (creature) {
        await this.addCreature(creature);
      }
    })
  }
  getList = (input: GetListInput) => {
    return this.service.getAllCreatures(this.campaign.id, this.encounter().id, input);
  }
  async addCreature(creature: Creature): Promise<void> {
      await firstValueFrom(this.service.addCreature(this.campaign.campaignTemplate.id, this.encounter().id, creature));
    this.encounter.set(await firstValueFrom(this.service.getById(this.campaign.campaignTemplate.id, this.encounter().id)));
  }
  async removeCreature(creatureId: string) {
      await firstValueFrom(this.service.removeCreature(this.campaign.campaignTemplate.id, this.encounter().id, creatureId));
    this.encounter.set(await firstValueFrom(this.service.getById(this.campaign.campaignTemplate.id, this.encounter().id)));
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
}

