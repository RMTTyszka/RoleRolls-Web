import { TestBed } from '@angular/core/testing';

import { CampaignSceneLogService } from './campaign-scene-log.service';

describe('CampaignSceneHistoryService', () => {
  let service: CampaignSceneLogService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CampaignSceneLogService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
