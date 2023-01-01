import { TestBed } from '@angular/core/testing';

import { PocketCampaignDetailsService } from './pocket-campaign-details.service';

describe('PocketCampaignDetailsService', () => {
  let service: PocketCampaignDetailsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PocketCampaignDetailsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
