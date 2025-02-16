import { TestBed } from '@angular/core/testing';

import { CreatureTypesService } from './creature-types.service';

describe('CreatureTypesService', () => {
  let service: CreatureTypesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CreatureTypesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
