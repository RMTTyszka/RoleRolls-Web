import {TestBed} from '@angular/core/testing';

import {LegacyMonstersBaseService} from './legacy-monsters-base.service';

describe('MonstersBaseService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: LegacyMonstersBaseService = TestBed.get(LegacyMonstersBaseService);
    expect(service).toBeTruthy();
  });
});
