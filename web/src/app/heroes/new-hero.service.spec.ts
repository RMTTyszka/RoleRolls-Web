import { TestBed } from '@angular/core/testing';

import { NewHeroService } from './new-hero.service';

describe('NewHeroService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: NewHeroService = TestBed.get(NewHeroService);
    expect(service).toBeTruthy();
  });
});
