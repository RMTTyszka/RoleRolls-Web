import { TestBed } from '@angular/core/testing';

import { WeaponModelService } from './weapon-model.service';

describe('WeaponModelService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: WeaponModelService = TestBed.get(WeaponModelService);
    expect(service).toBeTruthy();
  });
});
