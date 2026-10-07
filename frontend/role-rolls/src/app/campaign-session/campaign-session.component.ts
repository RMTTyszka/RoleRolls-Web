import { Component, ChangeDetectionStrategy } from '@angular/core';
import { CampaignWorkspaceComponent } from '@app/campaigns/campaign-workspace/campaign-workspace.component';

@Component({
  selector: 'rr-campaign-session',
  imports: [
    CampaignWorkspaceComponent
  ],
  templateUrl: './campaign-session.component.html',
  changeDetection: ChangeDetectionStrategy.Eager,
  styleUrl: './campaign-session.component.scss'
})
export class CampaignSessionComponent {}
