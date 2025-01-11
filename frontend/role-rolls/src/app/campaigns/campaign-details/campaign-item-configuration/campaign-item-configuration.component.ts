import { Component, Input } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { ItemConfigurationService } from './item-configuration.service';
import { firstValueFrom } from 'rxjs';
import { ItemConfigurationModel } from '../../models/item-configuration-model';
import { SubscriptionManager } from '../../../tokens/subscription-manager';
import { RR_API } from '../../../tokens/loh.api';
import { getAsForm } from '../../../tokens/EditorExtension';
import { Campaign } from '../../models/campaign';
import { PropertyType } from '../../models/propertyType';
import { PropertySelectorComponent } from '../../../components/property-selector/property-selector.component';
import { NgIf } from '@angular/common';
import { InputText } from 'primeng/inputtext';

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

  @Input() public campaign: Campaign;
  public form: FormGroup;
  public serverUrl = RR_API.backendUrl;
  public loaded: boolean = false;
  public armorDefense = 'Armor Defense';
  public defenses: any[] = [];
  public attributes: any[] = [];
  public subcriptionManager = new SubscriptionManager();
  constructor(private itemConfigurationService: ItemConfigurationService) {
  }



  async ngOnInit(): Promise<void> {
    const configuration = await firstValueFrom(this.itemConfigurationService.get(this.campaign.id));
    this.form = getAsForm(configuration);
    this.populateOptions();
    this.subcriptionManager.add('form.valueChanges', this.form.valueChanges.subscribe(() => {
      this.save();
    }));
    this.loaded = true;
  }
  ngOnDestroy(): void {
    this.subcriptionManager.clear();
  }

  populateOptions() {
    this.defenses = this.campaign.creatureTemplate.defenses.map(d => {
      return {
        label: d.name, value: d.id,
      };
    });
    this.attributes = this.campaign.creatureTemplate.attributes.map(d => {
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
