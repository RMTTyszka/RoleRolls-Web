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
import {ItemInstantiatorService} from 'src/app/pocket-role-rolls/items/item-instantiator/services/item-instantiator.service';
import {createForm} from 'src/app/shared/EditorExtension';

@Component({
  selector: 'rr-creature-inventory',
  standalone: true,
  imports: [
    TableModule,
    PanelModule
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
    private dialogService: DialogService,
    private itemInstantiatorService: ItemInstantiatorService,
  ) {
  }

  public ngOnInit(): void {
    this.subscriptionManager.add('itensArray', this.itensArray.valueChanges.subscribe(() => {
      this.itens = this.itensArray.controls.map((e) => {
        return {
          name: e.get('name').value,
          level: e.get('level').value,
        } as ItemModel;
      });
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
        this.itensArray.push(createForm(new UntypedFormGroup({}), item));
      }
    });
  }
  ngOnDestroy(): void {
    this.subscriptionManager.clear();
  }
}
