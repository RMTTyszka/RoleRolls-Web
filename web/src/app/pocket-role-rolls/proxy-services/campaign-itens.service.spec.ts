import { TestBed } from '@angular/core/testing';

import { CampaignItemTemplatesService } from './campaign-item-templates.service';

describe('CampaignItensService', () => {
  let service: CampaignItemTemplatesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CampaignItemTemplatesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
