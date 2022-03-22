import { Component, OnDestroy, OnInit } from '@angular/core';
import {Combat} from '../../shared/models/combat/Combat.model';
import {CombatManagementService} from '../../combat/combat-management.service';
import {Encounter} from '../../shared/models/Encounter.model';
import {CampaignEncounterService} from '../campaign-encounters/campaign-encounter.service';
import {CampaignSessionService} from '../campaign-session.service';
import { SubscriptionManager } from 'src/app/shared/utils/subscription-manager';

@Component({
  selector: 'rr-campaign-combat',
  templateUrl: './campaign-combat.component.html',
  styleUrls: ['./campaign-combat.component.css']
})
export class CampaignCombatComponent implements OnInit, OnDestroy {

  selectedCombat: Combat;

  private subscriptionManager = new SubscriptionManager();
  constructor(
    private combatService: CombatManagementService,
    private campaignEncounterService: CampaignEncounterService,
    private campaignSessionService: CampaignSessionService
  ) { }

  ngOnInit(): void {
    this.subscribeToCombatUpdated();
  }
  public ngOnDestroy(): void {
      this.subscriptionManager.clear();
  }

  public combatSelected(combat: Combat) {
    this.combatService.combatUpdated.next(combat);
  }
  public encounterSelected(encounter: Encounter) {
    this.campaignEncounterService.instantiate(this.campaignSessionService.campaign.id, encounter.id)
      .subscribe((combat: Combat) => this.combatSelected(combat));
  }
  private subscribeToCombatUpdated() {
    this.subscriptionManager.add('combatUpdated', this.combatService.combatUpdated.subscribe((combat: Combat) => {
      this.selectedCombat = combat;
    }));
  }

}
