import { TestBed } from '@angular/core/testing';

import { CreatureDetailsService } from './creature-details.service';

describe('CreatureDetailsService', () => {
  let service: CreatureDetailsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CreatureDetailsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
