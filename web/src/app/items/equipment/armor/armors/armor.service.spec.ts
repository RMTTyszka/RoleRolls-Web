import {TestBed} from '@angular/core/testing';

import {ArmorTemplateService} from './armor-template.service';

describe('ArmorService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ArmorTemplateService = TestBed.get(ArmorTemplateService);
    expect(service).toBeTruthy();
  });
});
