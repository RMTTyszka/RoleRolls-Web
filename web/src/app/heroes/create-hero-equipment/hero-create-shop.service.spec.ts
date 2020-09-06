import { TestBed } from '@angular/core/testing';

import { HeroCreateShopService } from './hero-create-shop.service';

describe('HeroCreateShopService', () => {
  let service: HeroCreateShopService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(HeroCreateShopService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
