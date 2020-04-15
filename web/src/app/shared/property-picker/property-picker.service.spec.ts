import {TestBed} from '@angular/core/testing';

import {PropertyPickerService} from './property-picker.service';

describe('PropertyPickerService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: PropertyPickerService = TestBed.get(PropertyPickerService);
    expect(service).toBeTruthy();
  });
});
