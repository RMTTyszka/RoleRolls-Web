import {Component, signal, WritableSignal} from '@angular/core';
import {GridComponent, RRColumns} from '@app/components/grid/grid.component';
import {CreatureCategory} from '@app/campaigns/models/CreatureCategory';
import {Campaign} from '@app/campaigns/models/campaign';
import {DynamicDialogConfig, DynamicDialogRef} from 'primeng/dynamicdialog';
import {CampaignCreaturesService} from '@services/campaign-creatures/campaign-creatures.service';
import {GetListInput} from '@app/tokens/get-list-input';
import {GetAllCampaignCreaturesInput} from '@app/models/campaign-creatures/get-all-campaign-creatures-input';
import {Creature} from '@app/campaigns/models/creature';
import {EncountersService} from '@app/encounters/services/encounters.service';
import {Encounter} from '@app/encounters/models/encounter';

@Component({
  selector: 'rr-encounter-select',
  imports: [
    GridComponent
  ],
  templateUrl: './encounter-select.component.html',
  styleUrl: './encounter-select.component.scss'
})
export class EncounterSelectComponent {
  columns: RRColumns[] = [];
  refreshGrid = signal<boolean>(false);
  isMaster: boolean;
  public campaign:  WritableSignal<Campaign> = signal<Campaign>(null);
  constructor(dialogConfig: DynamicDialogConfig,
              private service: EncountersService,
              private dialogRef: DynamicDialogRef<EncounterSelectComponent>
  ){
    this.campaign.set(dialogConfig.data.campaign);
    this.columns = this.buildColumns();
  }
  getList = (input: GetListInput) => {
    return this.service.getAll(this.campaign().id, input);
  }
  rowSelected(encounter: Encounter) {
    this.dialogRef.close(encounter);
  }
  private buildColumns() {
    return [{
      header: 'Name',
      property: 'name'
    } as RRColumns];
  }
}
