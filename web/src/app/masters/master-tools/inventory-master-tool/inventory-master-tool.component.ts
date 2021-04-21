import {Component, Input, OnInit} from '@angular/core';
import {Creature} from '../../../shared/models/creatures/Creature.model';
import {Combat} from '../../../shared/models/combat/Combat.model';
import {Inventory} from '../../../shared/models/Inventory.model';
import {ItemInstance} from '../../../shared/models/ItemInstance.model';
import {EquipableInstance} from '../../../shared/models/EquipableInstance.model';
import {FormGroup} from '@angular/forms';
import {DialogService} from 'primeng/dynamicdialog';
import {
  ItemInstantiatorModalData,
  ItemInstantiatorPath,
  MasterItemInstantiatorComponent
} from '../master-item-instantiator/master-item-instantiator.component';

@Component({
  selector: 'loh-inventory-master-tool',
  templateUrl: './inventory-master-tool.component.html',
  styleUrls: ['./inventory-master-tool.component.css']
})
export class InventoryMasterToolComponent implements OnInit {
  @Input() creature: Creature = new Creature();
  @Input() combat: Combat = new Combat();

  form: FormGroup = new FormGroup({});
  constructor(
    private dialog: DialogService,
  ) { }

  ngOnInit(): void {
  }
  get inventory(): Inventory {
    return this.creature.inventory;

  }

  showItem(item: ItemInstance) {
    return item.equipable && (item as EquipableInstance).removable || !item.equipable;
  }

  selectItemTemplate() {
    this.dialog.open(MasterItemInstantiatorComponent, {
      data: {
        creatureId: this.creature.id,
        combatId: this.combat.id,
        path: ItemInstantiatorPath.combat
      } as ItemInstantiatorModalData
    }).onClose.subscribe((creature: Creature) => {
      this.creature = creature;
    });
  }
}
