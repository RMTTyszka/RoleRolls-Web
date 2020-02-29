import { TestBed, inject } from '@angular/core/testing';

import { EncountersService } from './encounters.service';

describe('EncountersService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [EncountersService]
    });
  });

  it('should be created', inject([EncountersService], (service: EncountersService) => {
    expect(service).toBeTruthy();
  }));
});
