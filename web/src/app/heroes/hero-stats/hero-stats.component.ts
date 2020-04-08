import {Component, Input, OnInit} from '@angular/core';
import {CreatureStatus} from '../../shared/models/CreatureStatus.model';

@Component({
  selector: 'loh-hero-stats',
  templateUrl: './hero-stats.component.html',
  styleUrls: ['./hero-stats.component.css']
})
export class HeroStatsComponent implements OnInit {

  stats: string[] = ['defense', 'evasion', 'life', 'moral', 'dodge', 'specialAttack', 'magicDefense', 'mana', 'specialPower'];
  @Input() status: CreatureStatus;
  constructor(
  ) {

  }

  ngOnInit() {
  }

}
