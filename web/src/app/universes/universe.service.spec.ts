import { TestBed } from '@angular/core/testing';

import { UniverseService } from './universe.service';

describe('UniverseService', () => {
  let service: UniverseService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(UniverseService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
