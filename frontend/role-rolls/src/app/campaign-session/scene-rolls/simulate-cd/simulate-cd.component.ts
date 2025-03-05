import { Component, Input } from '@angular/core';
import { finalize, Subject } from 'rxjs';
import { SimulateCdInput } from '@app/campaigns/models/SimulateCdInput';
import { CampaignScene } from '@app/campaigns/models/campaign-scene-model';
import { CampaignsService } from '@app/campaigns/services/campaigns.service';
import { SubscriptionManager } from '@app/tokens/subscription-manager';
import { Campaign } from '@app/campaigns/models/campaign';
import { SimulateCdResult } from '@app/campaigns/models/simulate-cd-result';
import { ProgressSpinner } from 'primeng/progressspinner';
import { TableModule } from 'primeng/table';
import { FormsModule } from '@angular/forms';
import { ButtonDirective } from 'primeng/button';
import { NgIf } from '@angular/common';

@Component({
  selector: 'rr-simulate-cd',
  imports: [
    ProgressSpinner,
    TableModule,
    FormsModule,
    ButtonDirective,
    NgIf
  ],
  templateUrl: './simulate-cd.component.html',
  styleUrl: './simulate-cd.component.scss'
})
export class SimulateCdComponent {
  @Input() public simulateInputEmitter: Subject<SimulateCdInput>;
  @Input() public simulateResultEmitter: Subject<boolean>;
  @Input() public campaign: Campaign;
  @Input() public scene: CampaignScene;
  public chances: SimulateCdResult[] = [];
  public expectedChance: number;
  public input: SimulateCdInput;
  public calculating = false;
  private subscriptionManager: SubscriptionManager = new SubscriptionManager();
  constructor(
    private readonly campaignsService: CampaignsService
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
