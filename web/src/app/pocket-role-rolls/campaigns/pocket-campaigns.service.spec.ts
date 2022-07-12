import { TestBed } from '@angular/core/testing';

import { PocketCampaignsService } from './pocket-campaigns.service';

describe('PocketCampaignsService', () => {
  let service: PocketCampaignsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PocketCampaignsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
