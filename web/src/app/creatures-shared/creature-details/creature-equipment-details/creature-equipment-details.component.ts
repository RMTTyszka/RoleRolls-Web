import {Component, Input, OnInit} from '@angular/core';
import {Creature} from '../../../shared/models/creatures/Creature.model';

@Component({
  selector: 'loh-creature-equipment-details',
  templateUrl: './creature-equipment-details.component.html',
  styleUrls: ['./creature-equipment-details.component.css']
})
export class CreatureEquipmentDetailsComponent implements OnInit {
  @Input() public creature: Creature;

  constructor() { }

  ngOnInit(): void {
  }

}
