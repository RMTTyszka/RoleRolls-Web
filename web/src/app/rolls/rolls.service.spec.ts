import { TestBed } from '@angular/core/testing';

import { RollsService } from './rolls.service';

describe('RollsService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: RollsService = TestBed.get(RollsService);
    expect(service).toBeTruthy();
  });
});
