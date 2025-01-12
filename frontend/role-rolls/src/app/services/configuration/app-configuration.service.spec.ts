import { TestBed } from '@angular/core/testing';

import { AppConfigurationService } from './app-configuration.service';

describe('AppConfigurationService', () => {
  let service: AppConfigurationService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AppConfigurationService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
