import {Component, computed, effect, input, signal} from '@angular/core';
import { CreatureCategory } from '@app/campaigns/models/CreatureCategory';
import { ItemModel } from '@app/campaigns/models/item-model';
import { ItemConfigurationModel } from '@app/campaigns/models/item-configuration-model';
import { Creature } from '@app/campaigns/models/creature';
import { CampaignsService } from '@app/campaigns/services/campaigns.service';
import { EquipableSlot } from '@app/models/itens/equipable-slot';
import { Campaign } from '@app/campaigns/models/campaign';
import { CampaignScene } from '@app/campaigns/models/campaign-scene-model';
import { RROption } from '@app/models/RROption';
import { PropertyType } from '@app/campaigns/models/propertyType';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { getAsForm } from '@app/tokens/EditorExtension';
import { AttackInput } from '@app/campaigns/models/TakeDamangeInput';
import { CampaignSessionService } from '@app/campaign-session/campaign-session.service';
import { firstValueFrom } from 'rxjs';
import { WeaponCategory, WeaponTemplateModel } from '@app/models/itens/ItemTemplateModel';
import { NgIf } from '@angular/common';
import { Select } from 'primeng/select';
import { CreatureSelectComponent } from '@app/campaign-session/creature-select/creature-select.component';
import { PropertySelectorComponent } from '@app/components/property-selector/property-selector.component';
import { ButtonDirective } from 'primeng/button';
import { FormFieldWrapperComponent } from '@app/components/form-field-wrapper/form-field-wrapper.component';
import { FieldTitleDirective } from '@app/components/form-field-wrapper/field-title.directive';
import { AdvantageSelectComponent } from '@app/rolls/advantage-select/advantage-select.component';
import { LuckSelectComponent } from '@app/rolls/luck-select/luck-select.component';
import { Property } from '@app/models/bonuses/bonus';

@Component({
  selector: 'rr-attack',
  imports: [
    NgIf,
    ReactiveFormsModule,
    Select,
    CreatureSelectComponent,
    PropertySelectorComponent,
    ButtonDirective,
    FormFieldWrapperComponent,
    FieldTitleDirective,
    AdvantageSelectComponent,
    LuckSelectComponent
  ],
  templateUrl: './attack.component.html',
  styleUrl: './attack.component.scss'
})
export class AttackComponent {

  public attacker = input.required<Creature>();
  public showMe = input.required<boolean>();
  public campaign = input.required<Campaign>();
  public scene = input.required<CampaignScene>();
  public targets: Creature[] = [];
  public slotOptions: RROption<EquipableSlot>[] = [];
  public propertyType = PropertyType;
  public form = signal<FormGroup>(null);
  constructor(
    private pocketCampaignDetailsService: CampaignSessionService,
    private readonly campaignService: CampaignsService,
  ) {
    effect(() => {
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
        const itemConfiguration = this.campaign().campaignTemplate.itemConfiguration;
        const mainHand = attacker.equipment.mainHand;
        const hitProperty = this.resolveHitProperty(mainHand, itemConfiguration);
        const damageProperty = this.resolveDamageProperty(mainHand, itemConfiguration);
        const damageAttribute = this.resolveHitAttribute(damageProperty, attacker);
        const hitAttribute = this.resolveHitAttribute(hitProperty, attacker);
        this.form.set(getAsForm({
          slot: EquipableSlot.MainHand,
          vitality: null,
          defense: itemConfiguration.armorProperty,
          hitProperty: hitProperty,
          hitAttribute: hitAttribute,
          damageAttribute: damageAttribute,
          targetId: null,
          advantage: 0,
          luck: 0
        } as AttackInput, {
          requiredFields: ['hitAttribute', 'targetId', 'slot']
        }));
      };
    });
  }
  targetSelected(creature: Creature) {
    this.form().get('targetId').setValue(creature?.id);
  }

  private resolveTargets() {
    this.targets =
      this.attacker().category === CreatureCategory.Ally ?
        this.pocketCampaignDetailsService.monsters().concat(this.pocketCampaignDetailsService.heroes())
        : this.pocketCampaignDetailsService.heroes().concat(this.pocketCampaignDetailsService.monsters());
  }

  async attack() {
    const input = this.form().value as AttackInput;
    await firstValueFrom(this.campaignService.attack(this.campaign().id, this.scene().id, this.attacker().id, input));
  }

  private resolveHitProperty(mainHand: ItemModel, itemConfiguration: ItemConfigurationModel) {
    if (mainHand) {
      const weaponTemplate = mainHand.template as WeaponTemplateModel;
      switch (weaponTemplate.category) {
        case WeaponCategory.Light:
          return itemConfiguration.meleeLightWeaponHitProperty;
        case WeaponCategory.Medium:
          return itemConfiguration.meleeMediumWeaponHitProperty;
        case WeaponCategory.Heavy:
          return itemConfiguration.meleeHeavyWeaponHitProperty;

      }
    } else {
      return itemConfiguration.meleeLightWeaponHitProperty;
    }
    return itemConfiguration.meleeLightWeaponHitProperty;
  }
  private resolveDamageProperty(mainHand: ItemModel, itemConfiguration: ItemConfigurationModel) {
    if (mainHand) {
      const weaponTemplate = mainHand.template as WeaponTemplateModel;
      switch (weaponTemplate.category) {
        case WeaponCategory.Light:
          return itemConfiguration.meleeLightWeaponDamageProperty;
        case WeaponCategory.Medium:
          return itemConfiguration.meleeMediumWeaponDamageProperty;
        case WeaponCategory.Heavy:
          return itemConfiguration.meleeHeavyWeaponDamageProperty;
      }
    } else {
      return itemConfiguration.meleeLightWeaponDamageProperty;
    }
    return itemConfiguration.meleeLightWeaponDamageProperty;
  }

  private resolveHitAttribute(hitProperty: Property, attacker: Creature): Property | null {
    for (const skill of attacker.skills) {
      if (skill.skillTemplateId === hitProperty.id) {
        if (skill.attributeId) {
          return {
            id: this.resolveAttributeTemplateId(skill.attributeId),
            type: PropertyType.Attribute
          };
        }
        return null;
      }

      for (const specificSkill of skill.specificSkills) {
        if (specificSkill.specificSkillTemplateId === hitProperty.id) {
          if (specificSkill.attributeId) {
            return {
              id: this.resolveAttributeTemplateId(specificSkill.attributeId),
              type: PropertyType.Attribute
            };
          } else if (skill.attributeId) {
            return {
              id: this.resolveAttributeTemplateId(specificSkill.attributeId),
              type: PropertyType.Attribute
            }
          }
          return null;
        }
      }

      for (const attributelessSkill of attacker.attributelessSkills) {
        if (attributelessSkill.skillTemplateId === hitProperty.id) {
          return null;
        }

        for (const specificSkill of attributelessSkill.specificSkills) {
          if (specificSkill.specificSkillTemplateId === hitProperty.id) {
            return {
              id: this.resolveAttributeTemplateId(specificSkill.attributeId),
              type: PropertyType.Attribute,
            } as Property;
          }
        }
      }
      return null;
    }
    return null;
  }

  private resolveAttributeTemplateId(attributeId: string) {
      return this.attacker().attributes.find(a => a.id === attributeId).attributeTemplateId;
    }
  }

