import { ChangeDetectorRef, Component, signal } from '@angular/core';
import { Panel } from 'primeng/panel';
import { RadioButton } from 'primeng/radiobutton';
import { TableLazyLoadEvent, TableModule, TableRowSelectEvent } from 'primeng/table';
import { InputText } from 'primeng/inputtext';
import { NgForOf } from '@angular/common';
import { RRColumns } from '@app/components/grid/grid.component';
import {
  armorCategoryLabel,
  ArmorCategory,
  AnyItemTemplateModel,
  ItemTemplateModel,
  ItemType,
  weaponCategoryLabel,
  WeaponCategory
} from '@app/models/itens/ItemTemplateModel';
import { DialogService, DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { Creature } from '@app/campaigns/models/creature';
import { CampaignItemTemplatesService } from '@app/campaigns/campaign-details/campaign-itens/services/campaign-item-templates.service';
import { ItemInstanceService } from '@services/itens/instances/item-instance.service';
import { InstantiateItemInput } from '@app/models/itens/instances/instantiate-item-input';
import { CampaignSessionService } from '@app/campaign-session/campaign-session.service';
import {
  ConfirmNameAndLevelItemComponent
} from '@app/itens/confirm-name-and-level-item/confirm-name-and-level-item.component';
import { FormsModule } from '@angular/forms';
import { InputGroupAddonModule } from 'primeng/inputgroupaddon';

@Component({
  selector: 'rr-item-instantiator',
  imports: [
    Panel,
    RadioButton,
    TableModule,
    InputText,
    NgForOf,
    FormsModule,
    InputGroupAddonModule
  ],
  templateUrl: './item-instantiator.component.html',
  styleUrl: './item-instantiator.component.scss'
})
export class ItemInstantiatorComponent {
  private readonly defaultRows = 15;
  public columns: RRColumns[];
  public loading = false;
  public itemType = signal<ItemType | null>(ItemType.Consumable);
  public data: AnyItemTemplateModel[] = [];
  public totalCount: number;
  private creature: Creature;

  public itemTypesOptions = [
    {name: 'All', value: null},
    {name: 'Item', value: ItemType.Consumable},
    {name: 'Weapon', value: ItemType.Weapon},
    {name: 'Armor', value: ItemType.Armor},
  ];
  constructor(public ref: DynamicDialogRef,
              private dialogService: DialogService,
              public config: DynamicDialogConfig,
              private service: CampaignItemTemplatesService,
              private cdr: ChangeDetectorRef,
              private detailsServiceService: CampaignSessionService,
              private itemInstantiatorService: ItemInstanceService,
  ) {
  }
  ngOnInit() {
    if (this.config?.data) {
      this.creature = this.config.data['creature'] as Creature;
    }
    this.loading = true;
    this.get('', 0, this.defaultRows);
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
  }
  public onItemTypeChange(itemType: ItemType | null) {
    this.itemType.set(itemType);
    this.loading = true;
    this.get('', 0, this.defaultRows);
  }
  public get(filter?: string, skipCount?: number, maxResultCount?: number) {
    this.service.list(this.detailsServiceService.campaign.id, this.itemType(), filter, skipCount, maxResultCount).subscribe(response => {
      this.data = response.items;
      this.totalCount = response.totalCount;
      this.loading = false;
      this.cdr.detectChanges();
    }, error => {
      this.data = [];
      this.totalCount = 0;
      this.loading = false;
      this.cdr.detectChanges();
    });
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
  onLazyLoadEvent(event: TableLazyLoadEvent) {
    if (!event || Object.keys(event).length === 0) {
      return;
    }

    this.loading = true;
    this.cdr.detectChanges();
    const rows = event.rows ?? this.defaultRows;
    const first = event.first ?? 0;
    this.get('', first / rows, rows);
  }

  public rowSelected(event: TableRowSelectEvent) {
    this.dialogService.open(ConfirmNameAndLevelItemComponent, {
      data: {
        itemTemplate: event.data,
        level: this.creature.level
      }
    }).onClose.subscribe(async (input: InstantiateItemInput) => {
      if (input) {
        const item = await this.itemInstantiatorService.addItem(this.detailsServiceService.campaign.id, this.creature.id, input).toPromise();
        this.ref.close(item);
      } else {
        this.ref.close(null);
      }
    });
  }

}
