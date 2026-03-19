import {Component, computed, EventEmitter, signal, WritableSignal} from '@angular/core';
import {Fieldset} from "primeng/fieldset";
import {InputText} from "primeng/inputtext";
import {NgIf} from "@angular/common";
import {FormGroup, ReactiveFormsModule} from "@angular/forms";
import {Campaign} from '@app/campaigns/models/campaign';
import {EditorAction} from '@app/models/EntityActionData';
import {DialogService, DynamicDialogConfig, DynamicDialogRef} from 'primeng/dynamicdialog';
import {v4 as uuid} from 'uuid';
import {getAsForm} from '@app/tokens/EditorExtension';
import {canEditCampaign} from '@app/tokens/utils.funcs';
import {firstValueFrom, of} from 'rxjs';
import {Encounter} from '@app/encounters/models/encounter';
import {EncountersService} from '@app/encounters/services/encounters.service';
import {Creature} from '@app/campaigns/models/creature';
import {CreatureCategory} from '@app/campaigns/models/CreatureCategory';
import {CreatureSelectTableComponent} from '@app/creatures/creature-select-table/creature-select-table.component';
import {GridComponent, RRColumns, RRHeaderAction, RRTableAction} from '@app/components/grid/grid.component';
import {GetListInput} from '@app/tokens/get-list-input';
import {ButtonDirective} from 'primeng/button';
import {IftaLabelModule} from 'primeng/iftalabel';
import {PagedOutput} from '@app/models/PagedOutput';
import {CreatureDetailsService} from '@app/creatures/creature-details.service';

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
  public headerActions: RRHeaderAction[] = [];
  public creatureActions: RRTableAction<Creature>[] = [];
  public columns: RRColumns[] = [];
  public refreshGrid = new EventEmitter<void>();
  public form: FormGroup;
  public campaign: Campaign;
  public encounter: WritableSignal<Encounter>;
  public creatures = computed(() => this.encounter().creatures);
  public canEdit = false;
  private action: EditorAction = EditorAction.create;


  constructor(dialogConfig: DynamicDialogConfig,
              private service: EncountersService,
              private creatureDetailsService: CreatureDetailsService,
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

    this.form = getAsForm(this.encounter(), {
      requiredFields: ['name']
    });
    this.canEdit = canEditCampaign(this.campaign);
    if (!this.canEdit) {
      this.form.disable();
    }
    this.headerActions = this.buildHeaderActions();
    this.creatureActions = this.buildCreatureActions();
    this.columns = this.buildColumns();
    this.loaded.set(true);
  }
  public selectCreatureTemplate() {
    if (!this.canEdit) {
      return;
    }
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
    if (!this.canEdit) {
      return;
    }
    if (this.action === EditorAction.create) {
      this.encounter.set({
        ...this.encounter(),
        creatures: [...this.encounter().creatures, creature]
      });
    } else {
      await firstValueFrom(this.service.addCreature(this.campaign.id, this.encounter().id, creature));
    }
      this.refreshGrid.next();
  }
  async removeCreature(creatureId: string) {
    if (!this.canEdit) {
      return;
    }
    await firstValueFrom(this.service.removeCreature(this.campaign.id, this.encounter().id, creatureId, true));
    this.refreshGrid.next();
  }
  private buildHeaderActions() {
    return [
      {
        icon: 'pi pi-plus',
        condition: () => this.canEdit,
        tooltip: 'Add',
        callBack: () => this.selectCreatureTemplate(),
      } as RRHeaderAction
    ];
  }
  private buildCreatureActions() {
    return [
      {
        icon: 'pi pi-times',
        condition: () => this.canEdit,
        tooltip: 'Remove',
        callBack: (creature: Creature) => this.removeCreature(creature.id),
      } as RRTableAction<Creature>,
      {
        icon: 'pi pi-pencil',
        condition: () => this.canEdit,
        tooltip: 'Edit',
        csClass: 'p-button-danger',
        callBack: (creature: Creature) => this.editCreature(creature.id),
      } as RRTableAction<Creature>,
    ];
  }

  private buildColumns() {
    return [{
      header: 'Name',
      property: 'name'
    } as RRColumns];
  }

  public async save(): Promise<void> {
    if (!this.canEdit || !this.form.valid) {
      return;
    }
    const encounter = this.form.value as Encounter;
    encounter.creatures = this.creatures();
    const saveAction = this.action === EditorAction.create
      ? this.service.create(this.campaign.id, encounter)
      : this.service.update(this.campaign.id, encounter);

    await firstValueFrom(saveAction);
    this.dialogRef.close(true);
  }

  private async editCreature(id: string) {
    if (!this.canEdit) {
      return;
    }
    await firstValueFrom(this.creatureDetailsService.openCreatureEditor(this.campaign, id, CreatureCategory.Enemy, EditorAction.update));
  }
}

