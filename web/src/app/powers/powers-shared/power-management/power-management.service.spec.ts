import { TestBed } from '@angular/core/testing';

import { PowerManagementService } from './power-management.service';

describe('PowerManagementService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: PowerManagementService = TestBed.get(PowerManagementService);
    expect(service).toBeTruthy();
  });
});
