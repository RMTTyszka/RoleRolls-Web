import { TestBed } from '@angular/core/testing';

import { ItemTemplateApiService } from './item-template-api.service';

describe('ItemTemplateApiService', () => {
  let service: ItemTemplateApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ItemTemplateApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
