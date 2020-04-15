import {Component, Input, OnInit} from '@angular/core';
import {Initiative} from '../../shared/models/Iniciative.model';
import {Creature} from '../../shared/models/Creature.model';

@Component({
  selector: 'loh-initiative',
  templateUrl: './initiative.component.html',
  styleUrls: ['./initiative.component.css']
})
export class InitiativeComponent implements OnInit {

  @Input() initiatives: Initiative[] = [];
  @Input() currentCreatureId: string;
  constructor() { }

  ngOnInit() {
  }

  isCurrentCreatureTurn(creatureId: string) {
    return creatureId === this.currentCreatureId;
  }

}
