import { TestBed } from '@angular/core/testing';

import { ArmorService } from './armor.service';

describe('ArmorService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ArmorService = TestBed.get(ArmorService);
    expect(service).toBeTruthy();
  });
});
