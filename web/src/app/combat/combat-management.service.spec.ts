import { TestBed } from '@angular/core/testing';

import { CombatManagementService } from './combat-management.service';

describe('CombatManagementService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: CombatManagementService = TestBed.get(CombatManagementService);
    expect(service).toBeTruthy();
  });
});
