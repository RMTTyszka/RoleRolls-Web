import { ResolveFn, Router } from '@angular/router';
import { of } from 'rxjs';
import { Campaign } from '../../models/campaign';
import { inject } from '@angular/core';
import { CampaignsService } from '../../services/campaigns.service';
import {AuthenticationService} from '@app/authentication/services/authentication.service';
import {tap} from 'rxjs/operators';
import {CampaignTemplate} from '@app/campaigns/models/campaign.template';

export const campaignResolver: ResolveFn<Campaign> = (route, state) => {
  const campaignId = route.paramMap.get('campaignId');
  const campaignService = inject<CampaignsService>(CampaignsService)
  const authenticationService = inject<AuthenticationService>(AuthenticationService)
  const router = inject<Router>(Router)
  if (campaignId && campaignId === 'new') {
    router.navigate(['newCampaign']);
    return of(null);
  } else {
    return campaignService.get(campaignId).pipe(tap((campaign: Campaign) => {
      campaign.isMaster = authenticationService.isMaster(campaign.masterId);
    }));
  }
}
