import { TestBed } from '@angular/core/testing';

import { UniverseInterceptor } from './universe.interceptor';

describe('UniverseInterceptor', () => {
  beforeEach(() => TestBed.configureTestingModule({
    providers: [
      UniverseInterceptor
      ]
  }));

  it('should be created', () => {
    const interceptor: UniverseInterceptor = TestBed.inject(UniverseInterceptor);
    expect(interceptor).toBeTruthy();
  });
});
