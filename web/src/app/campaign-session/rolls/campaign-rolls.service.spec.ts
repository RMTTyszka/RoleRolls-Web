import { TestBed } from '@angular/core/testing';

import { CampaignRollsService } from './campaign-rolls.service';

describe('CampaignRollsService', () => {
  let service: CampaignRollsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CampaignRollsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
