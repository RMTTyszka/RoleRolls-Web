import { TestBed } from '@angular/core/testing';

import { HeroShopService } from './hero-shop.service';

describe('HeroShopService', () => {
  let service: HeroShopService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(HeroShopService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
