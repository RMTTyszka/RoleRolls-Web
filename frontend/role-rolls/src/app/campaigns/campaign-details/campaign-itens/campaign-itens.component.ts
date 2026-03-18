import { Component, signal } from '@angular/core';
import { TableLazyLoadEvent, TableModule, TableRowSelectEvent } from 'primeng/table';
import { RRColumns } from '@app/components/grid/grid.component';
import { EditorAction } from '@app/models/EntityActionData';
import { RRAction } from '@app/models/RROption';
import { AuthenticationService } from '@app/authentication/services/authentication.service';
import { CampaignItemTemplatesService } from './services/campaign-item-templates.service';
import { CampaignEditorDetailsServiceService } from '../services/campaign-editor-details-service.service';
import { Campaign } from '../../models/campaign';
import { NgForOf, NgIf, NgStyle } from '@angular/common';
import { Tooltip } from 'primeng/tooltip';
import { ButtonDirective } from 'primeng/button';
import { RadioButton } from 'primeng/radiobutton';
import { FormsModule } from '@angular/forms';
import { InputText } from 'primeng/inputtext';
import { CampaignItemCreatorComponent } from './campaign-item-creator/campaign-item-creator.component';
import {ActivatedRoute} from '@angular/router';
import {
  armorCategoryLabel,
  ArmorCategory,
  AnyItemTemplateModel,
  ItemTemplateModel,
  ItemType,
  weaponCategoryLabel,
  WeaponCategory
} from '@app/models/itens/ItemTemplateModel';
import { InputGroupAddonModule } from 'primeng/inputgroupaddon';
import { canEditCampaignConfiguration } from '@app/tokens/utils.funcs';

@Component({
  selector: 'rr-campaign-itens',
  standalone: true,

  templateUrl: './campaign-itens.component.html',
  imports: [
    TableModule,
    NgStyle,
    Tooltip,
    ButtonDirective,
    RadioButton,
    FormsModule,
    InputText,
    CampaignItemCreatorComponent,
    NgForOf,
    NgIf,
    InputGroupAddonModule
  ],
  styleUrl: './campaign-itens.component.scss'
})
export class CampaignItensComponent {
  private readonly defaultRows = 15;
  public first = 0;
  public data: AnyItemTemplateModel[] = [];
  public totalCount: number = 0;
  public loading: boolean = true;
  public columns: RRColumns[] = [];
  public actions: RRAction<ItemTemplateModel>[] = [];
  public campaign!: Campaign;
  public itemType = signal<ItemType | null>(ItemType.Consumable);
  public itemTypeEnum = ItemType;
  public itemTypesOptions = [
    {name: 'All', value: null},
    {name: 'Item', value: ItemType.Consumable},
    {name: 'Weapon', value: ItemType.Weapon},
    {name: 'Armor', value: ItemType.Armor},
  ];
  public get canEdit(): boolean {
    return Boolean(this.campaign && canEditCampaignConfiguration(this.campaign));
  }
  public isMaster(masterId: string): boolean {
    return this.authenticationService.userId === masterId;
  }

  constructor(
    private service: CampaignItemTemplatesService,
    private authenticationService: AuthenticationService,
    private detailsServiceService: CampaignEditorDetailsServiceService,
    private route: ActivatedRoute,
  ) {
    this.initGrid();
  }

  ngOnInit(): void {
    this.route.data.subscribe(data => {
      this.campaign = data['campaign'];
      this.first = 0;
      this.loading = true;
      this.get('', this.first, this.defaultRows);
    });

  }
  public onItemTypeChange(itemType: ItemType | null) {
    this.itemType.set(itemType);
    this.first = 0;
    this.loading = true;
    this.get('', this.first, this.defaultRows);
  }
  public rowSelected(event: TableRowSelectEvent) {
    this.detailsServiceService.itemTemplate.next(event.data);
    this.detailsServiceService.itemTemplateEditorAction.next(EditorAction.update);
  }
  public actionsWidth() {
    return {width: `${this.actions.length * 3}%`};
  }
  public resolve(column: RRColumns, obj: any) {
    const value = column.property.split('.').reduce(function(prev, curr) {
      return prev ? prev[curr] : null;
    }, obj || self);
    if (column.format) {
      return column.format(obj, value);
    }
    return value;
  }
  public get(filter?: string, skipCount?: number, maxResultCount?: number) {
    if (!this.campaign) {
      return;
    }

    this.service.list(this.campaign.id, this.itemType(), filter, skipCount, maxResultCount).subscribe(response => {
      this.data = response.items;
      this.totalCount = response.totalCount;
      this.loading = false;
    }, error => {
      this.data = [];
      this.totalCount = 0;
      this.loading = false;
    });
  }
  onLazyLoadEvent(event: TableLazyLoadEvent) {
    if (!this.campaign || !event || Object.keys(event).length === 0) {
      return;
    }

    this.loading = true;
    const rows = event.rows ?? this.defaultRows;
    const first = event.first ?? 0;
    this.first = first;
    this.get('', first, rows);
  }

  public refreshList() {
    this.loading = true;
    this.get('', this.first, this.defaultRows);
  }

  private initGrid() {
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
            return weaponCategoryLabel(value as WeaponCategory);
          }
          if (item.type === ItemType.Armor) {
            return armorCategoryLabel(value as ArmorCategory);
          }
          return '';
        }
      } as RRColumns,
      {
        header: 'Range',
        property: 'range',
        format: (item: AnyItemTemplateModel, value: string) => {
          return item.type === ItemType.Weapon ? value || '' : '';
        }
      } as RRColumns,
    ];
    this.actions = [
      {
        icon: 'pi pi-times-circle',
        callBack: ((entity: ItemTemplateModel) => {
          this.service.removeItem(entity.id, entity.type).subscribe(() => this.refreshList());
        }),
        condition: (() => {
          return this.canEdit;
        }),
        csClass: 'p-button-danger',
        tooltip: 'Remove'
      } as RRAction<ItemTemplateModel>
    ];
  }
}
