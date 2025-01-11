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
import {AttributeTemplateModel, DefenseTemplateModel} from '@app/campaigns/models/campaign-template.model';

@Component({
  selector: 'rr-campaign-item-configuration',
  standalone: true,
  templateUrl: './campaign-item-configuration.component.html',
  imports: [
    ReactiveFormsModule,
    PropertySelectorComponent,
    NgIf,
    InputText
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
  constructor(private itemConfigurationService: ItemConfigurationService,
              private route: ActivatedRoute,
              ) {
  }

  async ngOnInit(): Promise<void> {
    this.route.data.subscribe(data => {
      this.campaign = data['campaign'];
      this.form = getAsForm(this.campaign.itemConfiguration);
      this.populateOptions();
      this.loaded = true;
      this.subcriptionManager.add('form.valueChanges', this.form.valueChanges.subscribe(() => {
        this.save();
      }));
    });

  }
  ngOnDestroy(): void {
    this.subcriptionManager.clear();
  }

  populateOptions() {
    this.defenses = this.campaign.campaignTemplate.defenses.map((d: DefenseTemplateModel) => {
      return {
        label: d.name, value: d.id,
      };
    });
    this.attributes = this.campaign.campaignTemplate.attributes.map((d: AttributeTemplateModel) => {
      return {
        label: d.name, value: d.id,
      };
    });
    this.form.addControl('armorDefenseTitle', new FormControl('Armor Defense'));
    this.form.addControl('lightWeaponHitPropertyTitle', new FormControl('Light Weapon Hit Property'));
    this.form.addControl('mediumWeaponHitPropertyTitle', new FormControl('Medium Weapon Hit Property'));
    this.form.addControl('heavyWeaponHitPropertyTitle', new FormControl('Heavy Weapon Hit Property'));
    this.form.addControl('lightWeaponDamagePropertyTitle', new FormControl('Light Weapon Damage Property'));
    this.form.addControl('mediumWeaponDamagePropertyTitle', new FormControl('Medium Weapon Damage Property'));
    this.form.addControl('heavyWeaponDamagePropertyTitle', new FormControl('Heavy Weapon Damage Property'));
    this.form.addControl('heavyWeaponDamagePropertyTitle', new FormControl('Heavy Weapon Damage Property'));
    this.form.addControl('basicAttackTargetLifeTitle', new FormControl('Basic Attack Life'));
  }
  public async save(): Promise<void> {
    const configuration = this.form.value as ItemConfigurationModel;
    await firstValueFrom(this.itemConfigurationService.update(this.campaign.id, configuration));
  }

  protected readonly PropertyType = PropertyType;
}
