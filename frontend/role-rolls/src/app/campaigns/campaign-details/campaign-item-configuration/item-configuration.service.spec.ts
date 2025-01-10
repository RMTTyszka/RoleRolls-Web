import { TestBed } from '@angular/core/testing';

import { ItemConfigurationService } from './item-configuration.service';

describe('ItemConfigurationService', () => {
  let service: ItemConfigurationService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ItemConfigurationService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
