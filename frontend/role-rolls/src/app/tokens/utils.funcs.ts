import {Campaign} from '@app/campaigns/models/campaign';

export function safeCast<T>(value: unknown): T {
  return value as T;
}

export function canEditTemplate(campaign: Campaign) {
  return campaign.isMaster && !campaign.campaignTemplate.default;
}
export function canEditCampaign(campaign: Campaign) {
  return campaign.isMaster;
}
