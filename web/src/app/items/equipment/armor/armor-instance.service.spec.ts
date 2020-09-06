import { TestBed } from '@angular/core/testing';

import { ArmorInstanceService } from './armor-instance.service';

describe('ArmorInstanceService', () => {
  let service: ArmorInstanceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ArmorInstanceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
