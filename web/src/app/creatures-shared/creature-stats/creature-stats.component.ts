import {Component, Input, OnInit} from '@angular/core';
import {CreatureStatus} from '../../shared/models/creatures/CreatureStatus.model';

@Component({
  selector: 'loh-creature-stats',
  templateUrl: './creature-stats.component.html',
  styleUrls: ['./creature-stats.component.css']
})
export class CreatureStatsComponent implements OnInit {

  stats: string[] = ['defense', 'evasion', 'life', 'moral', 'dodge', 'specialAttack', 'magicDefense', 'mana', 'specialPower'];
  @Input() status: CreatureStatus;
  constructor(
  ) {

  }

  ngOnInit() {
  }

}
