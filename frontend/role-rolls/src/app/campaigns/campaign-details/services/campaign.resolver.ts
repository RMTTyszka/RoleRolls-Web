import { ResolveFn, Router } from '@angular/router';
import { of } from 'rxjs';
import { Campaign } from '../../models/campaign';
import { inject } from '@angular/core';
import { CampaignsService } from '../../services/campaigns.service';

export const campaignResolver: ResolveFn<Campaign> = (route, state) => {
  const campaignId = route.paramMap.get('campaignId');
  const campaignService = inject<CampaignsService>(CampaignsService)
  const router = inject<Router>(Router)
  if (campaignId && campaignId === 'new') {
    router.navigate(['newCampaign']);
    return of(null);
  } else {
    return campaignService.get(campaignId);
  }
}
