import {Component, Input} from '@angular/core';
import {CampaignSessionService} from '@app/campaign-session/campaign-session.service';
import {Campaign} from '@app/campaigns/models/campaign';
import { CampaignScene } from '@app/campaigns/models/campaign-scene-model';
import {Creature, Life} from '@app/campaigns/models/creature';
import { Subject } from 'rxjs';
import {TakeDamageInput} from '@app/campaigns/models/TakeDamangeInput';
import {SubscriptionManager} from '@app/tokens/subscription-manager';
import {CampaignsService} from '@app/campaigns/services/campaigns.service';
import {TakeDamageApiInput} from '@app/campaign-session/creature-actions/models/TakeDamageApiInput';

@Component({
  selector: 'rr-vitality-manager',
  imports: [],
  templateUrl: './vitality-manager.component.html',
  styleUrl: './vitality-manager.component.scss'
})
export class VitalityManagerComponent {

  @Input() public inputEmitter: Subject<TakeDamageInput>;
  @Input() public campaign: Campaign;
  @Input() public scene: CampaignScene;
  public creature: Creature;
  public damageInput: TakeDamageApiInput = new TakeDamageApiInput();
  public healInput: TakeDamageApiInput = new TakeDamageApiInput();
  private subscriptionManager = new SubscriptionManager();

  public get lifes(): Life[] {
    return this.creature?.lifes;
  }

  constructor(
    private readonly campaignService: CampaignsService,
    private readonly detailsService: CampaignSessionService
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

  public takeDamage(life: Life) {
    this.damageInput.lifeId = life.id;
    this.campaignService.takeDamage(this.campaign.id, this.scene.id, this.creature.id, this.damageInput)
      .subscribe(() => {
        this.damageInput.value = 0;
        this.damageInput.lifeId = null;
        this.detailsService.heroTookDamage.next();
      })
  }

  public heal(life: Life) {
    this.healInput.lifeId = life.id;
    this.campaignService.heal(this.campaign.id, this.scene.id, this.creature.id, this.healInput)
      .subscribe(() => {
        this.healInput.value = 0;
        this.healInput.lifeId = null;
        this.detailsService.heroTookDamage.next();
      })
  }

  public resultFromDamage(life: Life) {
    return Math.min(...[life.maxValue, life.value - this.damageInput.value])
  }

  public resultFromHeal(life: Life) {
    return Math.min(...[life.maxValue, life.value + this.healInput.value])
  }
}
