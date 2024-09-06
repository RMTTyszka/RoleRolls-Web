import { finalize } from 'rxjs/operators';
import { SubscriptionManager } from 'src/app/shared/utils/subscription-manager';
import { Component, Input, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { CampaignScene } from 'src/app/shared/models/pocket/campaigns/campaign-scene-model';
import { PocketCampaignModel } from 'src/app/shared/models/pocket/campaigns/pocket.campaign.model';
import {SimulateCdInput} from "../../../models/SimulateCdInput";
import {SimulateCdResult} from "../../../models/simulate-cd-result";
import {PocketCampaignsService} from "../../../pocket-campaigns.service";

@Component({
  selector: 'rr-simulate-cd',
  templateUrl: './simulate-cd.component.html',
  styleUrls: ['./simulate-cd.component.scss']
})
export class SimulateCdComponent implements OnInit {
  @Input() public simulateInputEmitter: Subject<SimulateCdInput>;
  @Input() public simulateResultEmitter: Subject<boolean>;
  @Input() public campaign: PocketCampaignModel;
  @Input() public scene: CampaignScene;
  public chances: SimulateCdResult[] = [];
  public expectedChance: number;
  public input: SimulateCdInput;
  public calculating = false;
  private subscriptionManager: SubscriptionManager = new SubscriptionManager();
  constructor(
    private readonly campaignsService: PocketCampaignsService
  ) { }

  ngOnInit(): void {
    this.simulateInputEmitter.subscribe((input: SimulateCdInput) => {
      this.input = input;
    })

  }
  public simulate() {
    this.calculating = true;
    this.input.expectedChance = this.expectedChance;
      this.campaignsService.simulateCd(this.campaign.id, this.scene.id, this.input)
      .pipe(finalize(() => this.calculating = false))
      .subscribe((result: SimulateCdResult[]) => {
        this.chances = result;
        }
      )
  }
}
