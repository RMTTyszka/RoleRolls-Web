import {Component, effect, Input, OnInit, signal} from '@angular/core';
import {LazyLoadEvent} from 'primeng/api';
import {
  ArmorCategory,
  ItemTemplateModel,
  ItemType,
  WeaponCategory
} from 'src/app/shared/models/pocket/itens/ItemTemplateModel';
import {RRAction, RRColumns} from 'src/app/shared/components/rr-grid/r-r-grid.component';
import {PocketCampaignModel} from 'src/app/shared/models/pocket/campaigns/pocket.campaign.model';
import {EditorAction} from 'src/app/shared/dtos/ModalEntityData';
import {CampaignItemTemplatesService} from '../../../proxy-services/campaign-item-templates.service';
import {AuthenticationService} from 'src/app/authentication/authentication.service';
import {
  CampaignEditorDetailsServiceService
} from 'src/app/pocket-role-rolls/campaigns/CampaignEditor/campaign-editor-details-service.service';

@Component({
  selector: 'rr-campaign-itens',
  templateUrl: './campaign-itens-template.component.html',
  styleUrls: ['./campaign-itens-template.component.scss']
})
export class CampaignItensTemplateComponent implements OnInit {
  public data: ItemTemplateModel[];
  public totalCount: number;
  public loading: boolean;
  public columns: RRColumns[] = [];
  public actions: RRAction<ItemTemplateModel>[];
  @Input() public campaign: PocketCampaignModel;
  public itemType = signal(ItemType.Consumable);
  public itemTypeEnum = ItemType;
  public itemTypesOptions = [
    {name: 'All', value: null},
    {name: 'Item', value: ItemType.Consumable},
    {name: 'Weapon', value: ItemType.Weapon},
    {name: 'Armor', value: ItemType.Armor},
  ];
  public isMaster(masterId: string): boolean {
    return this.authenticationService.userId === masterId;
  }

  constructor(
    private service: CampaignItemTemplatesService,
    private authenticationService: AuthenticationService,
    private detailsServiceService: CampaignEditorDetailsServiceService,
  ) {
    this.listenToItemTypeChanges();
  }

  ngOnInit(): void {
    this.columns = [
      {
        header: 'Name',
        property: 'name'
      } as RRColumns,
      {
        header: 'Type',
        property: 'type',
        format: (item: ItemTemplateModel, value: ItemType) => {
          switch (value) {
            case ItemType.Consumable:
              return 'Item';
            case ItemType.Weapon:
              return 'Weapon';
            case ItemType.Armor:
              return 'Armor';
          }
        }
      } as RRColumns,
      {
        header: 'Category',
        property: 'category',
        format: (item: ItemTemplateModel, value: WeaponCategory | ArmorCategory) => {
          if (item.type === ItemType.Weapon) {
            switch (value) {
              case WeaponCategory.Light:
                return 'Light';
              case WeaponCategory.Medium:
                return 'Medium';
              case WeaponCategory.Heavy:
                return 'Heavy';
            }
          }
          if (item.type === ItemType.Armor) {
            switch (value) {
              case ArmorCategory.Light:
                return 'Light';
              case ArmorCategory.Medium:
                return 'Medium';
              case ArmorCategory.Heavy:
                return 'Heavy';
            }
          }
        }
      } as RRColumns,
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
    ];
  }
  public rowSelected(event: {data: ItemTemplateModel}) {
    this.detailsServiceService.itemTemplate.next(event.data);
    this.detailsServiceService.itemTemplateEditorAction.next(EditorAction.update);
  }
  public actionsWidth() {
    return {width: `${this.actions.length * 3}%`};
  }
  public resolve(column: RRColumns, obj) {
    const value = column.property.split('.').reduce(function(prev, curr) {
      return prev ? prev[curr] : null;
    }, obj || self);
    if (column.format) {
      return column.format(obj, value);
    }
    return value;
  }
  public get(filter?: string, skipCount?: number, maxResultCount?: number) {
    this.service.list(this.campaign.id, this.itemType(), filter, skipCount, maxResultCount).subscribe(response => {
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
  private listenToItemTypeChanges() {
    effect(() => {
        this.get('', 0, 25);
    });
  }
}
