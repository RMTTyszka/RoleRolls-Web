import {ChangeDetectorRef, Component, effect, signal} from '@angular/core';
import {DialogService, DynamicDialogComponent, DynamicDialogRef} from 'primeng/dynamicdialog';
import {PocketCreature} from 'src/app/shared/models/pocket/creatures/pocket-creature';
import {NgForOf} from '@angular/common';
import {RadioButtonModule} from 'primeng/radiobutton';
import {TableModule} from 'primeng/table';
import {FormsModule} from '@angular/forms';
import {RRColumns} from 'src/app/shared/components/rr-grid/r-r-grid.component';
import {ArmorCategory, ItemTemplateModel, ItemType, WeaponCategory} from 'src/app/shared/models/pocket/itens/ItemTemplateModel';
import {LazyLoadEvent} from 'primeng/api';
import {CampaignItemTemplatesService} from 'src/app/pocket-role-rolls/proxy-services/campaign-item-templates.service';
import {AuthenticationService} from 'src/app/authentication/authentication.service';
import {
  CampaignEditorDetailsServiceService
} from 'src/app/pocket-role-rolls/campaigns/CampaignEditor/campaign-editor-details-service.service';
import {EditorAction} from 'src/app/shared/dtos/ModalEntityData';
import {
  PocketCampaignDetailsService
} from 'src/app/pocket-role-rolls/campaigns/CampaignInstance/pocket-campaign-bodyshell/pocket-campaign-details.service';
import {
  ConfirmNameAndLevelComponent
} from 'src/app/pocket-role-rolls/items/item-instantiator/confirm-name-and-level/confirm-name-and-level.component';
import {InstantiateItemInput} from 'src/app/pocket-role-rolls/items/item-instantiator/models/instantiate-item-input';
import {PanelModule} from 'primeng/panel';
import {InputTextModule} from 'primeng/inputtext';

@Component({
  selector: 'rr-item-instantiator',
  standalone: true,
  imports: [
    NgForOf,
    RadioButtonModule,
    TableModule,
    FormsModule,
    PanelModule,
    InputTextModule
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
  private creature: PocketCreature;

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
              private detailsServiceService: PocketCampaignDetailsService,
              ) {
    this.instance = this.dialogService.getInstance(this.ref);
  }
  ngOnInit() {
    if (this.instance && this.instance.data) {
      this.creature = this.instance.data['creature'] as PocketCreature;
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
        }
      } as RRColumns,
    ];
  }
  public get(filter?: string, skipCount?: number, maxResultCount?: number) {
    this.service.list(this.detailsServiceService.campaign.id, this.itemType(), filter, skipCount, maxResultCount).subscribe(response => {
      this.data = response.content;
      this.totalCount = response.totalElements;
      this.loading = false;
      this.cdr.detectChanges();
      }, error => {
      this.data = [];
      this.totalCount = 0;
      this.loading = false;
      this.cdr.detectChanges();
    });
  }
  onLazyLoadEvent(event: LazyLoadEvent) {
    this.loading = true;
    this.cdr.detectChanges();
    this.get('', event.first / event.rows, event.rows);
  }
  private listenToItemTypeChanges() {
    effect(() => {
      this.get('', 0, 25);
    });
  }

  ngOnDestroy() {
    if (this.ref) {
      this.ref.close();
    }
  }
  public rowSelected(event: {data: ItemTemplateModel}) {
    this.dialogService.open(ConfirmNameAndLevelComponent, {
      data: {
        itemTemplate: event.data,
        level: this.creature.level
      }
    }).onClose.subscribe((input: InstantiateItemInput) => {
      this.ref.close(input);
    });
  }
}
