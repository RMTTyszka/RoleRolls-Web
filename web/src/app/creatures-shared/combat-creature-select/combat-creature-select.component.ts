import {Component, Input, OnInit} from '@angular/core';
import {Hero} from '../../shared/models/Hero.model';
import {NewHero} from '../../shared/models/NewHero.model';

@Component({
  selector: 'loh-combat-creature-select',
  templateUrl: './combat-creature-select.component.html',
  styleUrls: ['./combat-creature-select.component.css']
})
export class CombatCreatureSelectComponent implements OnInit {

  @Input() creaturesOnCombat: NewHero[] = [];
  selectedCreature: NewHero;
  constructor() { }

  ngOnInit() {
  }

}
