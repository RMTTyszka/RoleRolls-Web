import {TestBed} from '@angular/core/testing';

import {MonsterBaseService} from './monster-model.service';

describe('MonsterBaseService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: MonsterBaseService = TestBed.get(MonsterBaseService);
    expect(service).toBeTruthy();
  });
});
