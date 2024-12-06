import { Component, input } from '@angular/core';
import {AttackInput} from '../../../models/TakeDamangeInput';
import {PocketCampaignModel} from '../../../../../shared/models/pocket/campaigns/pocket.campaign.model';
import {CampaignScene} from '../../../../../shared/models/pocket/campaigns/campaign-scene-model';

@Component({
  selector: 'rr-make-attack',
  standalone: true,
  imports: [],
  templateUrl: './make-attack.component.html',
  styleUrl: './make-attack.component.scss'
})
export class MakeAttackComponent {

  public attackInput = input<AttackInput>();
  public showMe = input<boolean>();
  public campaign = input<PocketCampaignModel>();
  public scene = input<CampaignScene>();
}
