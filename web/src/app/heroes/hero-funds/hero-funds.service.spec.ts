import { TestBed } from '@angular/core/testing';

import { HeroFundsService } from './hero-funds.service';

describe('HeroFundsService', () => {
  let service: HeroFundsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(HeroFundsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
