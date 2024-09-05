import { TestBed } from '@angular/core/testing';

import { CampaignItensService } from './campaign-itens.service';

describe('CampaignItensService', () => {
  let service: CampaignItensService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CampaignItensService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
