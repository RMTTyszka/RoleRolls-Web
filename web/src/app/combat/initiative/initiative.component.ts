import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
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
  @Output() currentCreatureOnTurnChanged = new EventEmitter<Creature>();
  constructor() { }

  ngOnInit() {
  }

  isCurrentCreatureTurn(creatureId: string) {
    return creatureId === this.currentCreatureId;
  }

}
