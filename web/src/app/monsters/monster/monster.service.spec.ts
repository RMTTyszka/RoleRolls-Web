import { TestBed } from '@angular/core/testing';

import { MonsterService } from './monster.service';

describe('MonsterService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: MonsterService = TestBed.get(MonsterService);
    expect(service).toBeTruthy();
  });
});
