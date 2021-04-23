import { Component, OnInit } from '@angular/core';
import {Combat} from '../../shared/models/combat/Combat.model';
import {CombatManagementService} from '../../combat/combat-management.service';

@Component({
  selector: 'rr-campaign-combat',
  templateUrl: './campaign-combat.component.html',
  styleUrls: ['./campaign-combat.component.css']
})
export class CampaignCombatComponent implements OnInit {

  selectedCombat: Combat;
  constructor(
    private combatService: CombatManagementService
  ) { }

  ngOnInit(): void {
  }

  combatSelected(combat: Combat) {
    this.selectedCombat = combat;
    this.combatService.combatUpdated.next(combat);
  }

}
