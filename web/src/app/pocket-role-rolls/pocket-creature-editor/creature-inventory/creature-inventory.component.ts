import {Component, effect, Input, OnDestroy, OnInit, Signal} from '@angular/core';
import {PocketCreature} from 'src/app/shared/models/pocket/creatures/pocket-creature';
import {ItemModel} from 'src/app/shared/models/pocket/creatures/item-model';
import {PocketInventory} from 'src/app/shared/models/pocket/creatures/pocket-inventory';
import {FormGroup, UntypedFormArray, UntypedFormControl, UntypedFormGroup} from '@angular/forms';
import {TableModule} from 'primeng/table';
import {PanelModule} from 'primeng/panel';
import {SubscriptionManager} from 'src/app/shared/utils/subscription-manager';
import {DialogService} from 'primeng/dynamicdialog';
import {ItemInstantiatorComponent} from 'src/app/pocket-role-rolls/items/item-instantiator/item-instantiator.component';
import {InstantiateItemInput} from 'src/app/pocket-role-rolls/items/item-instantiator/models/instantiate-item-input';
import {ItemInstanceService} from 'src/app/pocket-role-rolls/items/item-instantiator/services/item-instance.service';
import {createForm} from 'src/app/shared/EditorExtension';
import {ButtonDirective} from 'primeng/button';
import {NgIf, NgStyle} from '@angular/common';
import {PocketCampaignDetailsService} from '../../campaigns/CampaignInstance/pocket-campaign-bodyshell/pocket-campaign-details.service';
import {ConfirmationService} from 'primeng/api';

@Component({
  selector: 'rr-creature-inventory',
  standalone: true,
  imports: [
    TableModule,
    PanelModule,
    ButtonDirective,
    NgIf,
    NgStyle
  ],
  templateUrl: './creature-inventory.component.html',
  styleUrl: './creature-inventory.component.scss'
})
export class CreatureInventoryComponent implements OnInit, OnDestroy {

  @Input() public form: UntypedFormGroup;
  public itens: ItemModel[] = [];
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
    this.itens = this.itensArray.controls.map((e) => {
      return {
        name: e.get('name').value,
        level: e.get('level').value,
        id: e.get('id').value
      } as ItemModel;
    });
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
}
