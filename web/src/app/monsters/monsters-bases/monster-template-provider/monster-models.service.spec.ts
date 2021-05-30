import { TestBed } from '@angular/core/testing';

import { MonsterModelsService } from './monster-models.service';

describe('MonsterModelsService', () => {
  let service: MonsterModelsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MonsterModelsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
