import { Component, input, signal } from '@angular/core';
import { GridComponent, RRColumns, RRHeaderAction } from '@app/components/grid/grid.component';
import { Campaign } from '@app/campaigns/models/campaign';
import { DialogService, DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService } from '@app/authentication/services/authentication.service';
import { CampaignCreaturesService } from '@services/campaign-creatures/campaign-creatures.service';
import { GetAllCampaignCreaturesInput } from '@app/models/campaign-creatures/get-all-campaign-creatures-input';
import { Creature } from '@app/campaigns/models/creature';
import { CreatureEditorComponent } from '@app/creatures/creature-editor/creature-editor.component';
import { GetListInput } from '@app/tokens/get-list-input';
import { EditorAction } from '@app/models/EntityActionData';
import { CreatureCategory } from '@app/campaigns/models/CreatureCategory';

@Component({
  selector: 'rr-campaign-creatures',
  imports: [
    GridComponent
  ],
  templateUrl: './campaign-creatures.component.html',
  styleUrl: './campaign-creatures.component.scss'
})
export class CampaignCreaturesComponent {
  public creatureCategory = input<CreatureCategory>();
  headerActions: RRHeaderAction[] = [];
  columns: RRColumns[] = [];
  refreshGrid = signal<boolean>(false);
  private campaign: Campaign;
  isMaster = signal<boolean>(false);
  private dialogRef: DynamicDialogRef<CreatureEditorComponent>;


  constructor(private router: Router,
              private route: ActivatedRoute,
              private authenticationService: AuthenticationService,
              private dialogService: DialogService,
              public campaignCreaturesService: CampaignCreaturesService) {
    this.headerActions = this.buildHeaderActions();
    this.columns = this.buildColumns();
  }
  ngOnInit(): void {
    this.route.data.subscribe(data => {
      this.campaign = data['campaign'];
      this.isMaster.set(this.authenticationService.isMaster(this.campaign.masterId));
    });
  }
  getList = (input: GetListInput) => {
    const getCreaturesInput = {
      ...input,
      creatureCategory: this.creatureCategory()
    } as GetAllCampaignCreaturesInput;
    return this.campaignCreaturesService.getList(this.campaign.id, getCreaturesInput);
  }
  rowSelected(creature: Creature) {
    this.openCreatureDialog(creature);
  }

  private openCreatureDialog(creature: Creature): void {
    if (this.dialogRef) {
      this.dialogRef.close();
    }

    this.dialogRef = this.dialogService.open(CreatureEditorComponent, {
      data: {
        campaign: this.campaign,
        action: creature ? EditorAction.update : EditorAction.create,
        creatureId: creature?.id,
        creatureCategory: this.creatureCategory(),
      },
      position: 'right',
      height: '100%',
      width: '100vw',
      focusOnShow: false,
      closable: true,
      closeOnEscape: true,
      closeOnClickOutside: false,
      duplicate: true,
    } as DynamicDialogConfig);

    this.dialogRef.onClose.subscribe(() => {
      this.dialogRef = null;
    });
  }
  private buildHeaderActions() {
    return [
      {
        icon: 'pi pi-plus',
        condition: () => this.isMaster(),
        tooltip: 'New',
        callBack: () => this.openCreatureDialog(null),
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
