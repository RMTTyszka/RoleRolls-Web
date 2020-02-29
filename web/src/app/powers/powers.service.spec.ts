import { TestBed, inject } from '@angular/core/testing';

import { PowersService } from './powers.service';

describe('PowersService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [PowersService]
    });
  });

  it('should be created', inject([PowersService], (service: PowersService) => {
    expect(service).toBeTruthy();
  }));
});
