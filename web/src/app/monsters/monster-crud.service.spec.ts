import { TestBed } from '@angular/core/testing';

import { MonsterCrudService } from './monster-crud.service';

describe('MonsterCrudService', () => {
  let service: MonsterCrudService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MonsterCrudService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
