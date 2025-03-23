import { TestBed } from '@angular/core/testing';

import { EncountersService } from './encounters.service';

describe('EncountersService', () => {
  let service: EncountersService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EncountersService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
