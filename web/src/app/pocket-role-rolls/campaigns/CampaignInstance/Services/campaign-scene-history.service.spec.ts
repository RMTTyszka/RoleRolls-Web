import { TestBed } from '@angular/core/testing';

import { CampaignSceneHistoryService } from './campaign-scene-history.service';

describe('CampaignSceneHistoryService', () => {
  let service: CampaignSceneHistoryService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CampaignSceneHistoryService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
