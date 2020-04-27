import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {Hero} from '../../shared/models/NewHero.model';
import {Creature} from '../../shared/models/creatures/Creature.model';

@Component({
  selector: 'loh-combat-creature-select',
  templateUrl: './combat-creature-select.component.html',
  styleUrls: ['./combat-creature-select.component.css']
})
export class CombatCreatureSelectComponent implements OnInit {

  @Input() creaturesOnCombat: Creature[] = [];
  selectedCreature: Creature;
  @Output() creatureSelected = new EventEmitter<Creature>();
  @Input() placeholder: string;
  constructor() { }

  ngOnInit() {
  }

}
