import { TestBed } from '@angular/core/testing';

import { MonstersService } from './monsters.service';

describe('MonstersService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: MonstersService = TestBed.get(MonstersService);
    expect(service).toBeTruthy();
  });
});
