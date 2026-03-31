import { Component } from '@angular/core';
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
import { canEditCampaignConfiguration } from '@app/tokens/utils.funcs';

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
  public disabled: boolean = false;
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
      this.disabled = !canEditCampaignConfiguration(this.campaign);
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
    const readonlyTitleControls: Array<{ controlName: string; defaultValue: string }> = [
      { controlName: 'armorPropertyTitle', defaultValue: 'Armor Property' },
      { controlName: 'meleeLightWeaponHitPropertyTitle', defaultValue: 'Melee Light Weapon Hit Property' },
      { controlName: 'meleeMediumWeaponHitPropertyTitle', defaultValue: 'Melee Medium Weapon Hit Property' },
      { controlName: 'meleeHeavyWeaponHitPropertyTitle', defaultValue: 'Melee Heavy Weapon Hit Property' },
      { controlName: 'meleeLightWeaponDamagePropertyTitle', defaultValue: 'Melee Light Weapon Damage Property' },
      { controlName: 'meleeMediumWeaponDamagePropertyTitle', defaultValue: 'Melee Medium Weapon Damage Property' },
      { controlName: 'meleeHeavyWeaponDamagePropertyTitle', defaultValue: 'Melee Heavy Weapon Damage Property' },
      { controlName: 'rangedLightWeaponHitPropertyTitle', defaultValue: 'Ranged Light Weapon Hit Property' },
      { controlName: 'rangedMediumWeaponHitPropertyTitle', defaultValue: 'Ranged Medium Weapon Hit Property' },
      { controlName: 'rangedHeavyWeaponHitPropertyTitle', defaultValue: 'Ranged Heavy Weapon Hit Property' },
      { controlName: 'rangedLightWeaponDamagePropertyTitle', defaultValue: 'Ranged Light Weapon Damage Property' },
      { controlName: 'rangedMediumWeaponDamagePropertyTitle', defaultValue: 'Ranged Medium Weapon Damage Property' },
      { controlName: 'rangedHeavyWeaponDamagePropertyTitle', defaultValue: 'Ranged Heavy Weapon Damage Property' }
    ];

    readonlyTitleControls.forEach(({ controlName, defaultValue }) => {
      const existingValue = this.form.get(controlName)?.value;
      this.form.setControl(controlName, new FormControl({
        value: existingValue ?? defaultValue,
        disabled: true
      }));
    });
  }
  public async save(): Promise<void> {
    const configuration = this.form.getRawValue() as ItemConfigurationModel;
    await firstValueFrom(this.itemConfigurationService.update(this.campaign.id, configuration));
  }

  protected readonly PropertyType = PropertyType;
}
