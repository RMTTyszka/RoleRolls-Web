import {Component, effect, input, Input, OnInit, signal} from '@angular/core';
import {LazyLoadEvent} from "primeng/api";
import {ItemTemplateModel} from "src/app/shared/models/pocket/itens/ItemTemplateModel";
import {RRAction, RRColumns} from "src/app/shared/components/rr-grid/r-r-grid.component";
import {PocketCampaignModel} from "src/app/shared/models/pocket/campaigns/pocket.campaign.model";
import {EditorAction} from "src/app/shared/dtos/ModalEntityData";
import {CampaignItemTemplatesService} from "../../../proxy-services/campaign-item-templates.service";
import {AuthenticationService} from "src/app/authentication/authentication.service";
import {
  CampaignEditorDetailsServiceService
} from "src/app/pocket-role-rolls/campaigns/CampaignEditor/campaign-editor-details-service.service";

@Component({
  selector: 'rr-campaign-itens',
  templateUrl: './campaign-itens-templade.component.html',
  styleUrls: ['./campaign-itens-templade.component.scss']
})
export class CampaignItensTempladeComponent implements OnInit {
  public data: ItemTemplateModel[];
  public totalCount: number;
  public loading: boolean;
  public columns: RRColumns[] = [];
  public actions: RRAction<ItemTemplateModel>[];
  @Input() public campaign: PocketCampaignModel;
  public isMaster(masterId: string): boolean {
    return this.authenticationService.userId === masterId;
  }
  constructor(
    private service: CampaignItemTemplatesService,
    private authenticationService: AuthenticationService,
    private detailsServiceService: CampaignEditorDetailsServiceService,
  ) {
  }

  ngOnInit(): void {
    this.columns = [
      {
        header: 'Name',
        property: 'name'
      } as RRColumns
    ];
    this.actions = [
      {
        icon: 'pi pi-times-circle',
        callBack: ((entity: ItemTemplateModel) => {
          this.service.removeItem(entity.id).subscribe(() => this.get(null, 0, 25));
        }),
        condition: ((entity: ItemTemplateModel) => {
          return this.isMaster(this.campaign.masterId);
        }),
        csClass: 'p-button-danger',
        tooltip: 'Remove'
      } as RRAction<ItemTemplateModel>
    ]
  }
  public rowSelected(event: {data: ItemTemplateModel}) {
    this.detailsServiceService.itemTemplate.next(event.data);
    this.detailsServiceService.itemTemplateEditorAction.next(EditorAction.update);
  }
  public actionsWidth() {
    return {width: `${this.actions.length * 3}%`}
  }
  public resolve(path, obj) {
    return path.split('.').reduce(function(prev, curr) {
      return prev ? prev[curr] : null;
    }, obj || self);
  }
  public get(filter?: string, skipCount?: number, maxResultCount?: number) {
    this.service.list(this.campaign.id, filter, skipCount, maxResultCount).subscribe(response => {
      this.data = response.content;
      this.totalCount = response.totalElements;
      this.loading = false;
    }, error => {
      this.data = [];
      this.totalCount = 0;
      this.loading = false;
    });
  }
  onLazyLoadEvent(event: LazyLoadEvent) {
    this.loading = true;
    this.get('', event.first / event.rows, event.rows);
  }
}
