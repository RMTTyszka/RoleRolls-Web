import {TestBed} from '@angular/core/testing';

import {WeaponCategoryService} from './weapon-category.service';

describe('WeaponCategoryService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: WeaponCategoryService = TestBed.get(WeaponCategoryService);
    expect(service).toBeTruthy();
  });
});
