import { TestBed } from '@angular/core/testing';

import { MonstersBaseService } from './monsters-base.service';

describe('MonstersBaseService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: MonstersBaseService = TestBed.get(MonstersBaseService);
    expect(service).toBeTruthy();
  });
});
