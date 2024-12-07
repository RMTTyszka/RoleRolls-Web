import {Component, computed, input} from '@angular/core';
import {AttackInput} from '../../../models/TakeDamangeInput';
import {PocketCampaignModel, PropertyType} from '../../../../../shared/models/pocket/campaigns/pocket.campaign.model';
import {CampaignScene} from '../../../../../shared/models/pocket/campaigns/campaign-scene-model';
import {FormGroup, ReactiveFormsModule} from '@angular/forms';
import {getAsForm} from '../../../../../shared/EditorExtension';
import {PocketCreature} from '../../../../../shared/models/pocket/creatures/pocket-creature';
import {EquipableSlot} from '../../../../../shared/models/pocket/itens/equipable-slot';
import {DividerModule} from 'primeng/divider';
import {NgForOf, NgIf} from '@angular/common';
import {DropdownModule} from 'primeng/dropdown';
import {RROption} from 'src/app/pocket-role-rolls/global/models/RROption';
import {CampaignsModule} from 'src/app/pocket-role-rolls/campaigns/campaigns.module';
import {
  PocketCampaignDetailsService
} from 'src/app/pocket-role-rolls/campaigns/CampaignInstance/pocket-campaign-bodyshell/pocket-campaign-details.service';
import {CreatureType} from 'src/app/shared/models/creatures/CreatureType';
import {
  PocketCreatureSelectComponent
} from 'src/app/pocket-role-rolls/campaigns/CampaignInstance/pocket-creature-select/pocket-creature-select.component';
import {PropertySelectorComponent} from 'src/app/pocket-role-rolls/components/property-selector/property-selector.component';
import {ButtonDirective} from 'primeng/button';
import {PocketCampaignsService} from 'src/app/pocket-role-rolls/campaigns/pocket-campaigns.service';

@Component({
  selector: 'rr-make-attack',
  standalone: true,
  imports: [
    DividerModule,
    ReactiveFormsModule,
    DropdownModule,
    NgIf,
    PocketCreatureSelectComponent,
    PropertySelectorComponent,
    ButtonDirective,
    NgForOf,
  ],
  templateUrl: './make-attack.component.html',
  styleUrl: './make-attack.component.scss'
})
export class MakeAttackComponent {

  public attacker = input.required<PocketCreature>();
  public showMe = input.required<boolean>();
  public campaign = input.required<PocketCampaignModel>();
  public scene = input.required<CampaignScene>();
  public targets: PocketCreature[] = [];
  public slotOptions: RROption<EquipableSlot>[] = [];
  public propertyType = PropertyType;
  public form = computed<FormGroup>(() => {
    const attacker = this.attacker();
    if (attacker) {
      this.slotOptions = [];
      this.slotOptions.push({
        label: EquipableSlot[EquipableSlot.MainHand].toString(),
        value: EquipableSlot.MainHand,
      } as RROption<EquipableSlot>);
      if (attacker.equipment?.offHand) {
        this.slotOptions.push({
          label: EquipableSlot[EquipableSlot.OffHand].toString(),
          value: EquipableSlot.OffHand,
        } as RROption<EquipableSlot>);
      }
      this.resolveTargets();
      const itemConfiguration = this.campaign().itemConfiguration;
      return getAsForm({
        slot: EquipableSlot.MainHand,
        lifeId: itemConfiguration.basicAttackTargetLifeId,
        defenseId: itemConfiguration.armorDefenseId,
        targetId: null,
      } as AttackInput);
    }
  });
  constructor(
    private pocketCampaignDetailsService: PocketCampaignDetailsService,
  private readonly campaignService: PocketCampaignsService,
  ) {
  }

  targetSelected(creature: PocketCreature) {
    this.form().get('targetId').setValue(creature.id);
  }

  private resolveTargets() {
    this.targets =
    this.attacker().creatureType === CreatureType.Hero ?
      this.pocketCampaignDetailsService.monsters().concat(this.pocketCampaignDetailsService.heroes())
      : this.pocketCampaignDetailsService.heroes().concat(this.pocketCampaignDetailsService.monsters());
  }

  attack() {
    const input = this.form().value as AttackInput;
    this.campaignService.attack(this.campaign().id, this.scene().id, this.attacker().id, input);
  }
}
