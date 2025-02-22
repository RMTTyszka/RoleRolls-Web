import {Campaign} from '@app/campaigns/models/campaign';

export function safeCast<T>(value: unknown): T {
  return value as T;
}

export function canEdit(campaign: Campaign) {
  return campaign.isMaster && !campaign.campaignTemplate.default;
}
