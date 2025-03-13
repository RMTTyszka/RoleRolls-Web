import { TestBed } from '@angular/core/testing';

import { CampaignCreaturesService } from './campaign-creatures.service';

describe('CampaignCreaturesService', () => {
  let service: CampaignCreaturesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CampaignCreaturesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
