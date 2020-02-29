import { TestBed } from '@angular/core/testing';

import { RaceService } from './race.service';

describe('RaceService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: RaceService = TestBed.get(RaceService);
    expect(service).toBeTruthy();
  });
});
