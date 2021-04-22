import {Component, Input, OnInit} from '@angular/core';
import {Resistances} from '../../shared/models/Resistances.model';

@Component({
  selector: 'rr-creature-resistances',
  templateUrl: './creature-resistances.component.html',
  styleUrls: ['./creature-resistances.component.css']
})
export class CreatureResistancesComponent implements OnInit {

  resistancesList: string[] = ['fear', 'health', 'magic', 'physical', 'reflex'];
  @Input() resistances: Resistances;;
  constructor(
  ) {

  }

  ngOnInit() {
  }

}
