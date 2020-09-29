import { TestBed } from '@angular/core/testing';

import { MonsterManagementService } from './monster-management.service';

describe('MonsterManagementService', () => {
  let service: MonsterManagementService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MonsterManagementService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
