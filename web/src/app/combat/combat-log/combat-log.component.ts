import {Component, Input, OnInit} from '@angular/core';
import {CombatLog} from '../../shared/models/combat/CombatLog';

@Component({
  selector: 'rr-combat-log',
  templateUrl: './combat-log.component.html',
  styleUrls: ['./combat-log.component.css']
})
export class CombatLogComponent implements OnInit {

  @Input() combatLogs: CombatLog[];
  constructor() { }

  ngOnInit() {
  }

}
