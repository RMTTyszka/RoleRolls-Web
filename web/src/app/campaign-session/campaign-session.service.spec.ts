import { TestBed } from '@angular/core/testing';

import { CampaignSessionService } from './campaign-session.service';

describe('CampaignSessionService', () => {
  let service: CampaignSessionService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CampaignSessionService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
