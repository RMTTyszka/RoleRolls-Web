import { TestBed } from '@angular/core/testing';

import { ArmorCategoryService } from './armor-category.service';

describe('ArmorCategoryService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ArmorCategoryService = TestBed.get(ArmorCategoryService);
    expect(service).toBeTruthy();
  });
});
