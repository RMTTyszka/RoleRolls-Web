import { TestBed } from '@angular/core/testing';

import { ItemInstantiatorService } from './item-instantiator.service';

describe('ItemInstantiatorService', () => {
  let service: ItemInstantiatorService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ItemInstantiatorService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
