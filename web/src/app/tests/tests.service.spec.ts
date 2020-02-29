import { TestBed } from '@angular/core/testing';

import { TestsService } from './tests.service';

describe('TestsService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: TestsService = TestBed.get(TestsService);
    expect(service).toBeTruthy();
  });
});
