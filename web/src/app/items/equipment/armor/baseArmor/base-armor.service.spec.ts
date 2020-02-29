import { TestBed } from '@angular/core/testing';

import { BaseArmorService } from './base-armor.service';

describe('BaseArmorService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: BaseArmorService = TestBed.get(BaseArmorService);
    expect(service).toBeTruthy();
  });
});
