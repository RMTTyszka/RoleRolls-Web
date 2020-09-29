import {Component, Input, OnInit} from '@angular/core';
import {Creature} from '../../shared/models/creatures/Creature.model';

@Component({
  selector: 'loh-creature-details',
  templateUrl: './creature-details.component.html',
  styleUrls: ['./creature-details.component.css']
})
export class CreatureDetailsComponent implements OnInit {

  @Input() creature: Creature;
  constructor() { }

  ngOnInit(): void {
  }

}
