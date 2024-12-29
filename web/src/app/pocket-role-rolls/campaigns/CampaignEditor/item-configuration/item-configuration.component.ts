import {Component, Input, OnDestroy, OnInit} from '@angular/core';
import {FormControl, FormGroup, FormsModule, ReactiveFormsModule} from '@angular/forms';
import {ItemConfigurationService} from 'src/app/pocket-role-rolls/campaigns/CampaignEditor/item-configuration/item-configuration.service';
import {getAsForm} from 'src/app/shared/EditorExtension';
import {LOH_API} from 'src/app/loh.api';
import {firstValueFrom} from 'rxjs';
import {PocketCampaignModel, PropertyType} from 'src/app/shared/models/pocket/campaigns/pocket.campaign.model';
import {
  ItemConfigurationModel
} from 'src/app/pocket-role-rolls/campaigns/CampaignEditor/item-configuration/models/item-configuration-model';
import {NgIf} from '@angular/common';
import {InputGroupModule} from 'primeng/inputgroup';
import {InputTextModule} from 'primeng/inputtext';
import {InputGroupAddonModule} from 'primeng/inputgroupaddon';
import {DropdownItem, DropdownModule} from 'primeng/dropdown';
import {SubscriptionManager} from 'src/app/shared/utils/subscription-manager';
import {PropertySelectorComponent} from '../../../components/property-selector/property-selector.component';

@Component({
  selector: 'rr-item-configuration',
  standalone: true,
  imports: [
    NgIf,
    ReactiveFormsModule,
    InputGroupModule,
    InputTextModule,
    FormsModule,
    InputGroupAddonModule,
    DropdownModule,
    PropertySelectorComponent
  ],
  templateUrl: './item-configuration.component.html',
  styleUrl: './item-configuration.component.scss'
})
export class ItemConfigurationComponent implements OnInit, OnDestroy {

  @Input() public campaign: PocketCampaignModel;
  public form: FormGroup;
  public serverUrl = LOH_API.myPocketBackUrl;
  public loaded: boolean;
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
