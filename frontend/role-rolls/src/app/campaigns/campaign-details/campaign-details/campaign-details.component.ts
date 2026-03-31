import { Component } from '@angular/core';
import { CampaignWorkspaceComponent } from '@app/campaigns/campaign-workspace/campaign-workspace.component';

@Component({
  selector: 'rr-campaign-details',
  standalone: true,
  templateUrl: './campaign-details.component.html',
  imports: [
    CampaignWorkspaceComponent
  ],
  styleUrl: './campaign-details.component.scss'
})
export class CampaignDetailsComponent {}

