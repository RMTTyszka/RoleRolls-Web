import { TestBed } from '@angular/core/testing';

import { EquipableTemplateApiService } from './equipable-template-api.service';

describe('EquipableTemplateApiService', () => {
  let service: EquipableTemplateApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EquipableTemplateApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
