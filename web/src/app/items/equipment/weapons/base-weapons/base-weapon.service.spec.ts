import {TestBed} from '@angular/core/testing';

import {BaseWeaponService} from './base-weapon.service';

describe('BaseWeaponService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: BaseWeaponService = TestBed.get(BaseWeaponService);
    expect(service).toBeTruthy();
  });
});
