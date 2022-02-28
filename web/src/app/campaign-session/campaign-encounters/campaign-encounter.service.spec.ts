import { TestBed } from '@angular/core/testing';

import { CampaignEncounterService } from './campaign-encounter.service';

describe('CampaignEncounterService', () => {
  let service: CampaignEncounterService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CampaignEncounterService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
