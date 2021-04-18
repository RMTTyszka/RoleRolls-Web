import {Component, Input, OnInit} from '@angular/core';
import {Creature} from '../../../shared/models/creatures/Creature.model';
import {ItemInstance} from '../../../shared/models/ItemInstance.model';

@Component({
  selector: 'loh-creature-inventory-details',
  templateUrl: './creature-inventory-details.component.html',
  styleUrls: ['./creature-inventory-details.component.css']
})
export class CreatureInventoryDetailsComponent implements OnInit {
  @Input() public creature: Creature;
  @Input() public isMaster = false;
  public get itens(): ItemInstance[] {
    return this.creature.inventory.items;
  }
  constructor() { }

  ngOnInit(): void {
  }

}
