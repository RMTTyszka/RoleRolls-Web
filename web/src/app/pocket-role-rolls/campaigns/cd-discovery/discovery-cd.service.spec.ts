import { TestBed } from '@angular/core/testing';

import { DiscoveryCdService } from './discovery-cd.service';

describe('DiscoveryCdService', () => {
  let service: DiscoveryCdService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DiscoveryCdService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
