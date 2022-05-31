import { TestBed } from '@angular/core/testing';

import { PocketLoginService } from './pocket-login.service';

describe('PocketLoginService', () => {
  let service: PocketLoginService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PocketLoginService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
