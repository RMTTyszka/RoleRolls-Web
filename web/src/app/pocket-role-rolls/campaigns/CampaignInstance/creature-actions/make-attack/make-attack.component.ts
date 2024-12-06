import {Component, computed, input} from '@angular/core';
import {AttackInput} from '../../../models/TakeDamangeInput';
import {PocketCampaignModel} from '../../../../../shared/models/pocket/campaigns/pocket.campaign.model';
import {CampaignScene} from '../../../../../shared/models/pocket/campaigns/campaign-scene-model';
import {FormGroup} from '@angular/forms';
import {getAsForm} from '../../../../../shared/EditorExtension';
import {PocketCreature} from '../../../../../shared/models/pocket/creatures/pocket-creature';
import {EquipableSlot} from '../../../../../shared/models/pocket/itens/equipable-slot';

@Component({
  selector: 'rr-make-attack',
  standalone: true,
  imports: [],
  templateUrl: './make-attack.component.html',
  styleUrl: './make-attack.component.scss'
})
export class MakeAttackComponent {

  public attacker = input<PocketCreature>();
  public showMe = input<boolean>();
  public campaign = input<PocketCampaignModel>();
  public scene = input<CampaignScene>();
  public form = computed<FormGroup>(() => {
    const attacker = this.attacker();
    return getAsForm({
      slot: EquipableSlot.MainHand,
      lifeId: null,
      defenseId: null,
      targetId: null,
    } as AttackInput);
  });

  constructor() {
  }
}
