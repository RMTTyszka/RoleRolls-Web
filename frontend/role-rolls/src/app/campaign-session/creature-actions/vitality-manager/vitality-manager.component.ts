import {Component, Input} from '@angular/core';
import {CampaignSessionService} from '@app/campaign-session/campaign-session.service';
import {Campaign} from '@app/campaigns/models/campaign';
import { CampaignScene } from '@app/campaigns/models/campaign-scene-model';
import {Creature, Vitality} from '@app/campaigns/models/creature';
import { Subject } from 'rxjs';
import {TakeDamageInput} from '@app/campaigns/models/TakeDamangeInput';
import {SubscriptionManager} from '@app/tokens/subscription-manager';
import {CampaignsService} from '@app/campaigns/services/campaigns.service';
import {TakeDamageApiInput} from '@app/campaign-session/creature-actions/models/TakeDamageApiInput';
import { FormsModule } from '@angular/forms';
import { NgForOf } from '@angular/common';
import { ButtonDirective } from 'primeng/button';
import { Divider } from 'primeng/divider';
import { InputNumberModule } from 'primeng/inputnumber';

@Component({
  selector: 'rr-vitality-manager',
  imports: [
    FormsModule,
    NgForOf,
    ButtonDirective,
    Divider,
    InputNumberModule
  ],
  templateUrl: './vitality-manager.component.html',
  styleUrl: './vitality-manager.component.scss'
})
export class VitalityManagerComponent {

  @Input() public inputEmitter: Subject<TakeDamageInput>;
  @Input() public campaign: Campaign;
  @Input() public scene: CampaignScene;
  public creature: Creature;
  public damageInput: TakeDamageApiInput = new TakeDamageApiInput();
  public damageValue = 0;
  public damageVitalityId = '';
  public healVitalityId = '';
  public healValue = 0;
  public healInput: TakeDamageApiInput = new TakeDamageApiInput();
  private subscriptionManager = new SubscriptionManager();

  public get vitalities(): Vitality[] {
    return this.creature?.vitalities;
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

  public takeDamage(vitality: Vitality) {
    this.damageInput.vitalityId = vitality.id;
    this.damageInput.value = this.damageValue;
    this.campaignService.takeDamage(this.campaign.id, this.scene.id, this.creature.id, this.damageInput)
      .subscribe(() => {
        this.damageInput.value = 0;
        this.damageInput.vitalityId = null;
        this.detailsService.heroTookDamage.next();
      })
  }

  public heal(vitality: Vitality) {
    this.healInput.vitalityId = vitality.id;
    this.healInput.value = this.healValue;
    this.campaignService.heal(this.campaign.id, this.scene.id, this.creature.id, this.healInput)
      .subscribe(() => {
        this.healInput.value = 0;
        this.healInput.vitalityId = null;
        this.detailsService.heroTookDamage.next();
      })
  }

  public resultFromDamage(vitality: Vitality) {
    return Math.min(...[vitality.maxValue, vitality.value - this.damageInput.value])
  }

  public resultFromHeal(vitality: Vitality) {
    return Math.min(...[vitality.maxValue, vitality.value + this.healInput.value])
  }
}
