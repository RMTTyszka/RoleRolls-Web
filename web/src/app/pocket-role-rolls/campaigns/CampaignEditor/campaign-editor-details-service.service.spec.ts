import { TestBed } from '@angular/core/testing';

import { CampaignEditorDetailsServiceService } from './campaign-editor-details-service.service';

describe('CampaignEditorDetailsServiceService', () => {
  let service: CampaignEditorDetailsServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CampaignEditorDetailsServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
