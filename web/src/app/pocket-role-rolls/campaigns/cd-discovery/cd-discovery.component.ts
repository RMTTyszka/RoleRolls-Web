import { Component, OnInit } from '@angular/core';
import { FormGroup } from '../../../../../node_modules/@angular/forms';
import { DiscoveryCdInput } from 'src/app/pocket-role-rolls/campaigns/cd-discovery/tokens/discovery-cd-input';
import { RollsService } from 'src/app/rolls/rolls.service';
import { DiscoveryCdService } from 'src/app/pocket-role-rolls/campaigns/cd-discovery/discovery-cd.service';
import { PocketCampaignModel } from 'src/app/shared/models/pocket/campaigns/pocket.campaign.model';
import { PocketCreature } from 'src/app/shared/models/pocket/creatures/pocket-creature';
import { DynamicDialogConfig } from '../../../../../node_modules/primeng/dynamicdialog/public_api';
import { DiscoveryCdResult } from 'src/app/pocket-role-rolls/campaigns/cd-discovery/tokens/discovery-cd-result';

@Component({
  selector: 'rr-cd-discovery',
  templateUrl: './cd-discovery.component.html',
  styleUrls: ['./cd-discovery.component.scss']
})
export class CdDiscoveryComponent implements OnInit {

  public form: FormGroup;
  public campaign: PocketCampaignModel;
  public creature: PocketCreature;
  public results: DiscoveryCdResult[] = [];
  constructor(
    private readonly service: DiscoveryCdService,
    private dialogConfig: DynamicDialogConfig
  ) {
    this.creature = this.dialogConfig.data.creature;
    this.campaign = this.dialogConfig.data.campaign;
   }

  ngOnInit(): void {
  }

  public createForm() {

  }
  public getCd() {
    const input = this.form.value as DiscoveryCdInput;
    this.service.getChance(this.campaign.id, this.creature.id, input)
    .subscribe((result: DiscoveryCdResult[]) => {
      this.results = result;
    })
  }

}
