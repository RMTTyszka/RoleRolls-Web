import { Component, Input } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { ItemConfigurationService } from './item-configuration.service';
import { firstValueFrom } from 'rxjs';
import { ItemConfigurationModel } from '../../models/item-configuration-model';
import { SubscriptionManager } from '../../../tokens/subscription-manager';
import { getAsForm } from '../../../tokens/EditorExtension';
import { Campaign } from '../../models/campaign';
import { PropertyType } from '../../models/propertyType';
import { PropertySelectorComponent } from '../../../components/property-selector/property-selector.component';
import { NgIf } from '@angular/common';
import { InputText } from 'primeng/inputtext';
import {ActivatedRoute} from '@angular/router';
import {AttributeTemplate, DefenseTemplate} from '@app/campaigns/models/campaign.template';
import { AuthenticationService } from '@app/authentication/services/authentication.service';
import { InputGroupAddonModule } from 'primeng/inputgroupaddon';
import {InputGroup} from 'primeng/inputgroup';

@Component({
  selector: 'rr-campaign-item-configuration',
  standalone: true,
  templateUrl: './campaign-item-configuration.component.html',
  imports: [
    ReactiveFormsModule,
    PropertySelectorComponent,
    NgIf,
    InputText,
    InputGroupAddonModule,
    InputGroup
  ],
  styleUrl: './campaign-item-configuration.component.scss'
})
export class CampaignItemConfigurationComponent {

  public campaign: Campaign;
  public form: FormGroup;
  public loaded: boolean = false;
  public armorDefense = 'Armor Defense';
  public defenses: any[] = [];
  public attributes: any[] = [];
  public subcriptionManager = new SubscriptionManager();
  private disabled: boolean;
  public get default() {
    return this.campaign.campaignTemplate.default;
  }
  constructor(private itemConfigurationService: ItemConfigurationService,
              private route: ActivatedRoute,
              public authService: AuthenticationService,
              ) {
  }

  async ngOnInit(): Promise<void> {
    this.route.data.subscribe(data => {
      this.campaign = data['campaign'];
      this.form = getAsForm(this.campaign.campaignTemplate.itemConfiguration, {
        recursive: false
      });
      this.populateOptions();
      this.disabled = !this.authService.isMaster(this.campaign.masterId) || this.default;
      this.disabled = false;
      if (!this.disabled) {
        this.subcriptionManager.add('form.valueChanges', this.form.valueChanges.subscribe(async () => {
          await this.save();
        }));
      } else {
        this.form.disable();
      }
      this.loaded = true;
    });

  }
  ngOnDestroy(): void {
    this.subcriptionManager.clear();
  }

  populateOptions() {
    this.defenses = this.campaign.campaignTemplate.defenses.map((d: DefenseTemplate) => {
      return {
        label: d.name, value: d.id,
      };
    });
    this.attributes = this.campaign.campaignTemplate.attributes.map((d: AttributeTemplate) => {
      return {
        label: d.name, value: d.id,
      };
    });
    this.form.addControl('armorPropertyTitle', new FormControl('Armor Property'));
    this.form.addControl('basicAttackTargetFirstVitalityTitle', new FormControl('Basic Attack First Vitality'));
    this.form.addControl('meleeLightWeaponHitPropertyTitle', new FormControl('Melee Light Weapon Hit Property'));
    this.form.addControl('meleeMediumWeaponHitPropertyTitle', new FormControl('Melee Medium Weapon Hit Property'));
    this.form.addControl('meleeHeavyWeaponHitPropertyTitle', new FormControl('Melee Heavy Weapon Hit Property'));
    this.form.addControl('meleeLightWeaponDamagePropertyTitle', new FormControl('Melee Light Weapon Damage Property'));
    this.form.addControl('meleeMediumWeaponDamagePropertyTitle', new FormControl('Melee Medium Weapon Damage Property'));
    this.form.addControl('meleeHeavyWeaponDamagePropertyTitle', new FormControl('Melee Heavy Weapon Damage Property'));
    this.form.addControl('rangedLightWeaponHitPropertyTitle', new FormControl('Ranged Light Weapon Hit Property'));
    this.form.addControl('rangedMediumWeaponHitPropertyTitle', new FormControl('Ranged Medium Weapon Hit Property'));
    this.form.addControl('rangedHeavyWeaponHitPropertyTitle', new FormControl('Ranged Heavy Weapon Hit Property'));
    this.form.addControl('rangedLightWeaponDamagePropertyTitle', new FormControl('Ranged Light Weapon Damage Property'));
    this.form.addControl('rangedMediumWeaponDamagePropertyTitle', new FormControl('Ranged Medium Weapon Damage Property'));
    this.form.addControl('rangedHeavyWeaponDamagePropertyTitle', new FormControl('Ranged Heavy Weapon Damage Property'));
    this.form.addControl('basicAttackTargetSecondVitalityTitle', new FormControl('Basic Attack Second Vitality'));

  }
  public async save(): Promise<void> {
    const configuration = this.form.value as ItemConfigurationModel;
    await firstValueFrom(this.itemConfigurationService.update(this.campaign.id, configuration));
  }

  protected readonly PropertyType = PropertyType;
}
