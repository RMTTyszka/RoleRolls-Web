import { ResolveFn, Router } from '@angular/router';
import { of } from 'rxjs';
import { Campaign } from '../../models/campaign';
import { inject } from '@angular/core';
import { PocketCampaignsService } from '../../services/pocket-campaigns.service';

export const campaignResolver: ResolveFn<Campaign> = (route, state) => {
  const campaignId = route.paramMap.get('campaignId');
  const campaignService = inject<PocketCampaignsService>(PocketCampaignsService)
  const router = inject<Router>(Router)
  if (campaignId && campaignId === 'new') {
    router.navigate(['newCampaign']);
    return of(null);
  } else {
    return campaignService.get(campaignId);
  }
}
