import { Component, OnInit } from '@angular/core';
import {Combat} from '../../shared/models/combat/Combat.model';
import {CombatManagementService} from '../../combat/combat-management.service';
import {Encounter} from '../../shared/models/Encounter.model';
import {CampaignEncounterService} from '../campaign-encounters/campaign-encounter.service';
import {CampaignSessionService} from '../campaign-session.service';

@Component({
  selector: 'rr-campaign-combat',
  templateUrl: './campaign-combat.component.html',
  styleUrls: ['./campaign-combat.component.css']
})
export class CampaignCombatComponent implements OnInit {

  selectedCombat: Combat;
  constructor(
    private combatService: CombatManagementService,
    private campaignEncounterService: CampaignEncounterService,
    private campaignSessionService: CampaignSessionService
  ) { }

  ngOnInit(): void {
  }

  combatSelected(combat: Combat) {
    this.selectedCombat = combat;
    this.combatService.combatUpdated.next(combat);
  }
  encounterSelected(encounter: Encounter) {
    this.campaignEncounterService.instantiate(this.campaignSessionService.campaign.id, encounter.id)
      .subscribe((combat: Combat) => this.combatSelected(combat));
  }

}
