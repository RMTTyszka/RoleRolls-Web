import {Component, effect, Input, OnDestroy, OnInit, Signal} from '@angular/core';
import {PocketCreature} from 'src/app/shared/models/pocket/creatures/pocket-creature';
import {ItemModel} from 'src/app/shared/models/pocket/creatures/item-model';
import {PocketInventory} from 'src/app/shared/models/pocket/creatures/pocket-inventory';
import {FormGroup, UntypedFormArray, UntypedFormGroup} from '@angular/forms';
import {TableModule} from 'primeng/table';
import {PanelModule} from 'primeng/panel';
import {SubscriptionManager} from 'src/app/shared/utils/subscription-manager';
import {DialogService} from 'primeng/dynamicdialog';
import {ItemInstantiatorComponent} from 'src/app/pocket-role-rolls/items/item-instantiator/item-instantiator.component';

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
  public get itensArray() {
    return this.form?.get('inventory.itens') as UntypedFormArray;
  }
  constructor(private dialogService: DialogService) {
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
      data: {
        creature: this.creature
      }
    })
  }
  ngOnDestroy(): void {
    this.subscriptionManager.clear();
  }
}
