import { Subject } from 'rxjs';
import { Component, OnInit, Input, OnDestroy } from '@angular/core';
import { CampaignScene } from 'src/app/shared/models/pocket/campaigns/campaign-scene-model';
import { PocketCampaignDetailsService } from '../pocket-campaign-bodyshell/pocket-campaign-details.service';
import {TakeDamageInput} from "../../models/TakeDamangeInput";
import {PocketCampaignModel} from "../../../../shared/models/pocket/campaigns/pocket.campaign.model";
import {PocketCreature, PocketLife} from "../../../../shared/models/pocket/creatures/pocket-creature";
import {TakeDamageApiInput} from "../../models/TakeDamageApiInput";
import {SubscriptionManager} from "../../../../shared/utils/subscription-manager";
import {PocketCampaignsService} from "../../pocket-campaigns.service";

@Component({
  selector: 'rr-pocket-take-damage',
  templateUrl: './pocket-take-damage.component.html',
  styleUrls: ['./pocket-take-damage.component.scss']
})
export class PocketTakeDamageComponent implements OnInit, OnDestroy {

  @Input() public inputEmitter: Subject<TakeDamageInput>;
  @Input() public campaign: PocketCampaignModel;
  @Input() public scene: CampaignScene;
  public creature: PocketCreature;
  public damageInput: TakeDamageApiInput = new TakeDamageApiInput();
  public healInput: TakeDamageApiInput = new TakeDamageApiInput();
  private subscriptionManager = new SubscriptionManager();
  public get lifes(): PocketLife[] {
    return this.creature?.lifes;
  }

  constructor(
    private readonly campaignService: PocketCampaignsService,
    private readonly detailsService: PocketCampaignDetailsService
  ) {
    this.damageInput.value = 0;
    this.healInput.value = 0;
   }

  ngOnInit(): void {
    this.subscriptionManager.add('TakeDamageInput', this.inputEmitter.subscribe((input: TakeDamageInput) => {
      this.creature = input.creature;
    }))
  }
  ngOnDestroy(): void {
    this.subscriptionManager.clear();
  }

  public takeDamage(life: PocketLife) {
    this.damageInput.lifeId = life.id;
    this.campaignService.takeDamage(this.campaign.id, this.scene.id, this.creature.id, this.damageInput)
    .subscribe(() => {
      this.damageInput.value = 0;
      this.damageInput.lifeId = null;
      this.detailsService.heroTookDamage.next();
    })
  }

  public heal(life: PocketLife) {
    this.healInput.lifeId = life.id;
    this.campaignService.heal(this.campaign.id, this.scene.id, this.creature.id, this.healInput)
    .subscribe(() => {
      this.healInput.value = 0;
      this.healInput.lifeId = null;
      this.detailsService.heroTookDamage.next();
    })
  }

  public resultFromDamage(life: PocketLife) {
    return Math.min(...[life.maxValue, life.value - this.damageInput.value])
  }

  public resultFromHeal(life: PocketLife) {
    return Math.min(...[life.maxValue, life.value + this.healInput.value])
  }

}
