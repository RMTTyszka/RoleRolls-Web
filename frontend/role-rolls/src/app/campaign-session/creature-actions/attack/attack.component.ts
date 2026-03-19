import { Component, effect, input, signal } from '@angular/core';
import { NgIf } from '@angular/common';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { firstValueFrom, Subscription } from 'rxjs';
import { Select } from 'primeng/select';
import { ButtonDirective } from 'primeng/button';

import { CampaignSessionService } from '@app/campaign-session/campaign-session.service';
import { CreatureSelectComponent } from '@app/campaign-session/creature-select/creature-select.component';
import { PropertyByIdSelectorComponent } from '@app/components/property-by-id-selector/property-by-id-selector.component';
import { PropertySelectorComponent } from '@app/components/property-selector/property-selector.component';
import { FormFieldWrapperComponent } from '@app/components/form-field-wrapper/form-field-wrapper.component';
import { FieldTitleDirective } from '@app/components/form-field-wrapper/field-title.directive';
import { Campaign } from '@app/campaigns/models/campaign';
import { CampaignScene } from '@app/campaigns/models/campaign-scene-model';
import { CreatureCategory } from '@app/campaigns/models/CreatureCategory';
import { Creature } from '@app/campaigns/models/creature';
import { ItemConfigurationModel } from '@app/campaigns/models/item-configuration-model';
import { ItemModel } from '@app/campaigns/models/item-model';
import { PropertyType } from '@app/campaigns/models/propertyType';
import { AttackInput } from '@app/campaigns/models/TakeDamangeInput';
import { CampaignsService } from '@app/campaigns/services/campaigns.service';
import { RROption } from '@app/models/RROption';
import { EquipableSlot } from '@app/models/itens/equipable-slot';
import { WeaponCategory, WeaponTemplateModel } from '@app/models/itens/ItemTemplateModel';
import { Property } from '@app/models/bonuses/bonus';
import { AdvantageSelectComponent } from '@app/rolls/advantage-select/advantage-select.component';
import { LuckSelectComponent } from '@app/rolls/luck-select/luck-select.component';
import { getAsForm } from '@app/tokens/EditorExtension';

@Component({
  selector: 'rr-attack',
  imports: [
    NgIf,
    ReactiveFormsModule,
    Select,
    CreatureSelectComponent,
    PropertyByIdSelectorComponent,
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
  public form = signal<FormGroup | null>(null);

  private formSubscriptions = new Subscription();

  constructor(
    private pocketCampaignDetailsService: CampaignSessionService,
    private readonly campaignService: CampaignsService,
  ) {
    effect(() => {
      const attacker = this.attacker();
      const campaign = this.campaign();

      if (!attacker || !campaign) {
        this.formSubscriptions.unsubscribe();
        this.formSubscriptions = new Subscription();
        this.form.set(null);
        return;
      }

      this.slotOptions = this.buildSlotOptions(attacker);
      this.resolveTargets();
      this.initializeForm(attacker, campaign.campaignTemplate.itemConfiguration);
    });
  }

  ngOnDestroy() {
    this.formSubscriptions.unsubscribe();
  }

  targetSelected(creature: Creature) {
    this.form()?.get('targetId')?.setValue(creature?.id ?? null);
  }

  async attack() {
    const form = this.form();
    if (!form) {
      return;
    }

    const input = form.getRawValue() as AttackInput;
    await firstValueFrom(this.campaignService.attack(this.campaign().id, this.scene().id, this.attacker().id, input));
  }

  private initializeForm(attacker: Creature, itemConfiguration: ItemConfigurationModel) {
    this.formSubscriptions.unsubscribe();
    this.formSubscriptions = new Subscription();

    const initialSlot = EquipableSlot.MainHand;
    const form = getAsForm({
      slot: initialSlot,
      vitality: itemConfiguration.basicAttackTargetFirstVitality ?? null,
      defense: itemConfiguration.armorDefense1 ?? null,
      hitProperty: null,
      hitAttribute: null,
      damageAttribute: null,
      targetId: null,
      advantage: 0,
      luck: 0
    } as AttackInput, {
      requiredFields: ['targetId', 'slot']
    });

    this.form.set(form);
    this.syncDerivedFields(form, attacker, itemConfiguration);

    const slotControl = form.get('slot') as FormControl;
    const hitPropertyControl = form.get('hitProperty') as FormControl;

    this.formSubscriptions.add(
      slotControl.valueChanges.subscribe((slot: EquipableSlot | null) => {
        this.syncDerivedFields(form, attacker, itemConfiguration, slot ?? EquipableSlot.MainHand);
      })
    );

    this.formSubscriptions.add(
      hitPropertyControl.valueChanges.subscribe((property: Property | null) => {
        this.syncAttributeControl(
          form.get('hitAttribute') as FormControl,
          property,
          attacker,
          true
        );
      })
    );
  }

  private syncDerivedFields(
    form: FormGroup,
    attacker: Creature,
    itemConfiguration: ItemConfigurationModel,
    slot: EquipableSlot = EquipableSlot.MainHand
  ) {
    const weapon = this.resolveWeapon(attacker, slot);
    const hitProperty = this.resolveHitProperty(weapon, itemConfiguration);
    const damageProperty = this.resolveDamageProperty(weapon, itemConfiguration);

    form.patchValue({
      hitProperty
    }, { emitEvent: false });

    this.syncAttributeControl(
      form.get('hitAttribute') as FormControl,
      hitProperty,
      attacker,
      true
    );
    this.syncAttributeControl(
      form.get('damageAttribute') as FormControl,
      damageProperty,
      attacker,
      true
    );
  }

  private syncAttributeControl(
    control: FormControl,
    property: Property | null,
    attacker: Creature,
    requiredForSkill: boolean
  ) {
    control.clearValidators();

    if (!property || property.type === PropertyType.Attribute) {
      control.setValue(null, { emitEvent: false });
      control.updateValueAndValidity({ emitEvent: false });
      return;
    }

    const resolvedAttribute = this.resolveSpecificSkillAttribute(property, attacker);
    if (resolvedAttribute) {
      control.setValue(resolvedAttribute, { emitEvent: false });
      control.updateValueAndValidity({ emitEvent: false });
      return;
    }

    if (requiredForSkill && property.type === PropertyType.Skill) {
      control.addValidators(Validators.required);
    }

    const currentValue = control.value as Property | null;
    if (!currentValue || currentValue.type !== PropertyType.Attribute) {
      control.setValue(null, { emitEvent: false });
    }

    control.updateValueAndValidity({ emitEvent: false });
  }

  private buildSlotOptions(attacker: Creature): RROption<EquipableSlot>[] {
    const options = [{
      label: EquipableSlot[EquipableSlot.MainHand].toString(),
      value: EquipableSlot.MainHand,
    } as RROption<EquipableSlot>];

    if (attacker.equipment?.offHand) {
      options.push({
        label: EquipableSlot[EquipableSlot.OffHand].toString(),
        value: EquipableSlot.OffHand,
      } as RROption<EquipableSlot>);
    }

    return options;
  }

  private resolveTargets() {
    const targets = this.attacker().category === CreatureCategory.Ally ?
      this.pocketCampaignDetailsService.monsters().concat(this.pocketCampaignDetailsService.heroes())
      : this.pocketCampaignDetailsService.heroes().concat(this.pocketCampaignDetailsService.monsters());
    this.targets = targets;
  }

  private resolveWeapon(attacker: Creature, slot: EquipableSlot): ItemModel | null {
    switch (slot) {
      case EquipableSlot.OffHand:
        return attacker.equipment?.offHand ?? null;
      case EquipableSlot.MainHand:
      default:
        return attacker.equipment?.mainHand ?? null;
    }
  }

  private resolveHitProperty(weapon: ItemModel | null, itemConfiguration: ItemConfigurationModel): Property | null {
    if (weapon) {
      const weaponTemplate = weapon.template as WeaponTemplateModel;
      switch (weaponTemplate.category) {
        case WeaponCategory.Light:
          return itemConfiguration.meleeLightWeaponHitProperty ?? null;
        case WeaponCategory.Medium:
          return itemConfiguration.meleeMediumWeaponHitProperty ?? null;
        case WeaponCategory.Heavy:
          return itemConfiguration.meleeHeavyWeaponHitProperty ?? null;
        case WeaponCategory.LightShield:
          return itemConfiguration.meleeLightWeaponHitProperty ?? null;
        case WeaponCategory.MediumShield:
          return itemConfiguration.meleeMediumWeaponHitProperty ?? null;
        case WeaponCategory.HeavyShield:
          return itemConfiguration.meleeHeavyWeaponHitProperty ?? null;
      }
    }

    return itemConfiguration.meleeLightWeaponHitProperty ?? null;
  }

  private resolveDamageProperty(weapon: ItemModel | null, itemConfiguration: ItemConfigurationModel): Property | null {
    if (weapon) {
      const weaponTemplate = weapon.template as WeaponTemplateModel;
      switch (weaponTemplate.category) {
        case WeaponCategory.Light:
          return itemConfiguration.meleeLightWeaponDamageProperty ?? null;
        case WeaponCategory.Medium:
          return itemConfiguration.meleeMediumWeaponDamageProperty ?? null;
        case WeaponCategory.Heavy:
          return itemConfiguration.meleeHeavyWeaponDamageProperty ?? null;
      }
    }

    return itemConfiguration.meleeLightWeaponDamageProperty ?? null;
  }

  private resolveSpecificSkillAttribute(property: Property | null, attacker: Creature): Property | null {
    if (!property || property.type !== PropertyType.SpecificSkill) {
      return null;
    }

    const specificSkill = attacker.skills
      .flatMap(skill => skill.specificSkills)
      .find(skill => skill.specificSkillTemplateId === property.id);

    if (!specificSkill?.attributeId) {
      return null;
    }

    return this.toAttributeProperty(specificSkill.attributeId);
  }

  private toAttributeProperty(attributeId: string): Property | null {
    const attributeTemplateId = this.attacker().attributes.find(a => a.id === attributeId)?.attributeTemplateId;
    return attributeTemplateId ? {
      id: attributeTemplateId,
      type: PropertyType.Attribute
    } : null;
  }
}
