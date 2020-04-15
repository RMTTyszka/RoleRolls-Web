import {TestBed} from '@angular/core/testing';

import {MakeTestService} from './make-test.service';

describe('MakeTestService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: MakeTestService = TestBed.get(MakeTestService);
    expect(service).toBeTruthy();
  });
});
