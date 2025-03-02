import { TestBed } from '@angular/core/testing';

import { ItemInstanceService } from './item-instance.service';

describe('ItemInstantiatorService', () => {
  let service: ItemInstanceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ItemInstanceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
