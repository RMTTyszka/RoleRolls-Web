import {Component, Input, OnInit} from '@angular/core';
import {LazyLoadEvent} from "primeng/api";
import {EditorAction} from "../../../shared/dtos/ModalEntityData";
import {ItemTemplateModel} from "../../../shared/models/pocket/itens/ItemTemplateModel";
import {CampaignItemTemplatesService} from "../../proxy-services/campaign-item-templates.service";
import {RRAction, RRColumns} from "../../../shared/components/rr-grid/r-r-grid.component";
import {PocketCampaignModel} from "../../../shared/models/pocket/campaigns/pocket.campaign.model";
import {AuthenticationService} from "../../../authentication/authentication.service";

@Component({
  selector: 'rr-campaign-itens',
  templateUrl: './campaign-itens.component.html',
  styleUrls: ['./campaign-itens.component.scss']
})
export class CampaignItensComponent implements OnInit {
  public data: ItemTemplateModel[];
  public totalCount: number;
  public loading: boolean;
  public columns: RRColumns[] = [];
  public actions: RRAction<ItemTemplateModel>[];
  @Input() private campaign: PocketCampaignModel = new PocketCampaignModel();
  public selectedItem: ItemTemplateModel;
  public action: EditorAction;
  public isMaster(masterId: string): boolean {
    return this.authenticationService.userId === masterId;
  }
  constructor(
    private service: CampaignItemTemplatesService,
    private authenticationService: AuthenticationService,
  ) { }

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
          this.service.delete(entity.id);
        }),
        condition: ((entity: ItemTemplateModel) => {
          return this.isMaster(this.campaign.masterId);
        }),
        csClass: 'p-button-danger',
        tooltip: 'Remove'
      } as RRAction<ItemTemplateModel>
    ]
  }
  rowSelected(event: {data: ItemTemplateModel}) {
    this.selectedItem = event.data;
    this.action = EditorAction.update;
  }
  public actionsWidth() {
    return {width: `${this.actions.length * 3}%`}
  }
  resolve(path, obj) {
    return path.split('.').reduce(function(prev, curr) {
      return prev ? prev[curr] : null;
    }, obj || self);
  }
  get(filter?: string, skipCount?: number, maxResultCount?: number) {
    this.service.list(filter, skipCount, maxResultCount).subscribe(response => {
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
