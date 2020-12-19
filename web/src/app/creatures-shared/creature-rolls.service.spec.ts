import { TestBed } from '@angular/core/testing';

import { CreatureRollsService } from './creature-rolls.service';

describe('CreatureRollsService', () => {
  let service: CreatureRollsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CreatureRollsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
