import { Component, input, signal, WritableSignal } from '@angular/core';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { Campaign } from '@app/campaigns/models/campaign';
import { GridComponent, RRColumns, RRHeaderAction } from '@app/components/grid/grid.component';
import { GetListInput } from '@app/tokens/get-list-input';
import { Encounter } from '@app/encounters/models/encounter';
import { EncounterEditorComponent } from '@app/encounters/components/encounter-editor/encounter-editor.component';
import { CampaignCreaturesService } from '@services/campaign-creatures/campaign-creatures.service';
import { Creature } from '@app/campaigns/models/creature';
import { GetAllCampaignCreaturesInput } from '@app/models/campaign-creatures/get-all-campaign-creatures-input';
import { CreatureCategory } from '@app/campaigns/models/CreatureCategory';

@Component({
  selector: 'rr-creature-select-table',
  imports: [
    GridComponent
  ],
  templateUrl: './creature-select-table.component.html',
  styleUrl: './creature-select-table.component.scss'
})
export class CreatureSelectTableComponent {
  columns: RRColumns[] = [];
  refreshGrid = signal<boolean>(false);
  isMaster: boolean;
  public creatureCategory = signal<CreatureCategory>(null);
  public campaign:  WritableSignal<Campaign> = signal<Campaign>(null);
  constructor(dialogConfig: DynamicDialogConfig,
              private service: CampaignCreaturesService,
              private dialogRef: DynamicDialogRef<CreatureSelectTableComponent>
  ){
  this.campaign.set(dialogConfig.data.campaign);
  this.creatureCategory.set(dialogConfig.data.creatureCategory);
    this.columns = this.buildColumns();
  }
  getList = (input: GetListInput) => {
    const getCreaturesInput = {
      ...input,
      creatureCategory: this.creatureCategory(),
      onlyTemplates: true
    } as GetAllCampaignCreaturesInput;
    return this.service.getList(this.campaign().id, getCreaturesInput);
  }
  rowSelected(creature: Creature) {
    this.dialogRef.close(creature);
  }
  private buildColumns() {
    return [{
      header: 'Name',
      property: 'name'
    } as RRColumns];
  }
}
