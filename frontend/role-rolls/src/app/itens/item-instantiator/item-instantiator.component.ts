import { ChangeDetectorRef, Component, effect, signal } from '@angular/core';
import { Panel } from 'primeng/panel';
import { RadioButton } from 'primeng/radiobutton';
import { TableLazyLoadEvent, TableModule, TableRowSelectEvent } from 'primeng/table';
import { InputText } from 'primeng/inputtext';
import { NgForOf } from '@angular/common';
import { RRColumns } from '@app/components/grid/grid.component';
import { ArmorCategory, ItemTemplateModel, ItemType, WeaponCategory } from '@app/models/itens/ItemTemplateModel';
import { DialogService, DynamicDialogComponent, DynamicDialogRef } from 'primeng/dynamicdialog';
import { Creature } from '@app/campaigns/models/creature';
import { CampaignItemTemplatesService } from '@app/campaigns/campaign-details/campaign-itens/services/campaign-item-templates.service';
import { ItemInstanceService } from '@services/itens/instances/item-instance.service';
import { LazyLoadEvent } from 'primeng/api';
import { InstantiateItemInput } from '@app/models/itens/instances/instantiate-item-input';
import { CampaignSessionService } from '@app/campaign-session/campaign-session.service';
import {
  ConfirmNameAndLevelItemComponent
} from '@app/itens/confirm-name-and-level-item/confirm-name-and-level-item.component';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'rr-item-instantiator',
  imports: [
    Panel,
    RadioButton,
    TableModule,
    InputText,
    NgForOf,
    FormsModule
  ],
  templateUrl: './item-instantiator.component.html',
  styleUrl: './item-instantiator.component.scss'
})
export class ItemInstantiatorComponent {
  public columns: RRColumns[];
  public loading = false;
  public itemType = signal(ItemType.Consumable);
  public data: ItemTemplateModel[] = [];
  public totalCount: number;
  instance: DynamicDialogComponent | undefined;
  private creature: Creature;

  public itemTypesOptions = [
    {name: 'All', value: null},
    {name: 'Item', value: ItemType.Consumable},
    {name: 'Weapon', value: ItemType.Weapon},
    {name: 'Armor', value: ItemType.Armor},
  ];
  constructor(public ref: DynamicDialogRef,
              private dialogService: DialogService,
              private service: CampaignItemTemplatesService,
              private cdr: ChangeDetectorRef,
              private detailsServiceService: CampaignSessionService,
              private itemInstantiatorService: ItemInstanceService,
  ) {
    this.instance = this.dialogService.getInstance(this.ref);
    this.listenToItemTypeChanges();
  }
  ngOnInit() {
    if (this.instance && this.instance.data) {
      this.creature = this.instance.data['creature'] as Creature;
    }
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
          return '';
        }
      } as RRColumns,
    ];
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
    this.loading = true;
    this.cdr.detectChanges();
    this.get('', event.first / event.rows, event.rows);
  }
  private listenToItemTypeChanges() {
    effect(() => {
      this.get('', 0, 25);
    });
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
