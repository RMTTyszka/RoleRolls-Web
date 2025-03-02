import { Component } from '@angular/core';
import { EquipInput } from '@app/models/creatures/equip-input';
import { firstValueFrom } from 'rxjs';
import {EquipableSlot} from '@app/models/itens/equipable-slot';
import {ItemModel} from '@app/models/itens/instances/item-model';
import { ItemType } from '@app/models/itens/ItemTemplateModel';
import { createForm } from '@app/tokens/EditorExtension';
import { UntypedFormGroup } from '@angular/forms';

@Component({
  selector: 'rr-creature-inventory',
  imports: [],
  templateUrl: './creature-inventory.component.html',
  styleUrl: './creature-inventory.component.scss'
})
export class CreatureInventoryComponent {

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
    private creatureDetailsService: CreatureDetailsService,
  ) {
  }

  public ngOnInit(): void {
    this.populateItens();
    this.subscriptionManager.add('itensArray', this.itensArray.valueChanges
      .pipe(debounceTime(100))
      .subscribe(() => {
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
    this.creatureDetailsService.refreshCreature.next();
  }
}
