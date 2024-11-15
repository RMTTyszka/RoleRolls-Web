import {Component, Input, OnDestroy, OnInit} from '@angular/core';
import {PocketCreature} from 'src/app/shared/models/pocket/creatures/pocket-creature';
import {ItemModel} from 'src/app/shared/models/pocket/creatures/item-model';
import {UntypedFormArray, UntypedFormGroup} from '@angular/forms';
import {TableModule} from 'primeng/table';
import {PanelModule} from 'primeng/panel';
import {SubscriptionManager} from 'src/app/shared/utils/subscription-manager';
import {DialogService} from 'primeng/dynamicdialog';
import {ItemInstantiatorComponent} from 'src/app/pocket-role-rolls/items/item-instantiator/item-instantiator.component';
import {ItemInstanceService} from 'src/app/pocket-role-rolls/items/item-instantiator/services/item-instance.service';
import {createForm} from 'src/app/shared/EditorExtension';
import {ButtonDirective} from 'primeng/button';
import {NgIf} from '@angular/common';
import {PocketCampaignDetailsService} from '../../campaigns/CampaignInstance/pocket-campaign-bodyshell/pocket-campaign-details.service';
import {ConfirmationService} from 'primeng/api';
import {CreatureEquipmentComponent} from '../creature-equipment/creature-equipment.component';
import {ItemType} from '../../../shared/models/pocket/itens/ItemTemplateModel';
import {EquipInput} from '../tokens/equip-input';
import {EquipableSlot} from '../../../shared/models/pocket/itens/equipable-slot';
import {firstValueFrom} from 'rxjs';

@Component({
  selector: 'rr-creature-inventory',
  standalone: true,
  imports: [
    TableModule,
    PanelModule,
    ButtonDirective,
    NgIf,
    CreatureEquipmentComponent
  ],
  templateUrl: './creature-inventory.component.html',
  styleUrl: './creature-inventory.component.scss'
})
export class CreatureInventoryComponent implements OnInit, OnDestroy {

  @Input() public form: UntypedFormGroup;
  public itens: ItemModel[] = [];
  public itemType = ItemType;
  public subscriptionManager = new SubscriptionManager();
  @Input() public creature: PocketCreature;
  public get itensArray() {
    return this.form?.get('inventory.items') as UntypedFormArray;
  }
  constructor(
    private itemInstanceService: ItemInstanceService,
    private dialogService: DialogService,
    private confirmationService: ConfirmationService,
    private campaignDetailsService: PocketCampaignDetailsService,
  ) {
  }

  public ngOnInit(): void {
    this.populateItens();
    this.subscriptionManager.add('itensArray', this.itensArray.valueChanges.subscribe(() => {
    this.populateItens();
    }));
  }
  public instanteItem() {
    this.dialogService.open(ItemInstantiatorComponent, {
      width: '80%',
      height: '95%',
      baseZIndex: 10000,
      data: {
        creature: this.creature
      }
    }).onClose.subscribe((item: ItemModel) => {
      if (item) {
        const newItemForm = new UntypedFormGroup({});
        createForm(newItemForm, item);
        this.itensArray.push(newItemForm);
      }
    });
  }
  ngOnDestroy(): void {
    this.subscriptionManager.clear();
  }

  private populateItens() {
    this.itens = this.itensArray.value;
  }

  async remove(item: ItemModel, index: number) {
    this.confirmationService.confirm({
      header: 'Remove Item?',
      acceptLabel: 'Confirm',
      rejectLabel: 'Cancel',
      message: 'The item will be destroyed',

      accept: async () => {
        await this.itemInstanceService.removeItem(this.campaignDetailsService.campaign.id, this.creature.id, item.id).toPromise();
        this.itensArray.removeAt(index);
      }
    });

  }

  equip(item: ItemModel, index: number) {
    switch (item.template.type) {
      case ItemType.Consumable:
        break;
      case ItemType.Weapon:
        this.equipWeapon(item, index);
        break;
      case ItemType.Armor:
        break;
    }
  }

  private equipWeapon(item: ItemModel, index: number) {
    this.confirmationService.confirm({
      header: 'Equip ' + item.name,
      acceptLabel: 'MainHand',
      rejectLabel: 'OffHand',
      message: 'In wich hand would you like to equip',

      accept: async () => {
        await this.equipItem(index, {
          itemId: item.id,
          slot: EquipableSlot.MainHand
        } as EquipInput);
      }
    });
  }
  private async equipItem(index: number, input: EquipInput) {
    await firstValueFrom(this.itemInstanceService.equip(this.campaignDetailsService.campaign.id, this.creature.id, input));
    this.itensArray.removeAt(index);
  }
}
