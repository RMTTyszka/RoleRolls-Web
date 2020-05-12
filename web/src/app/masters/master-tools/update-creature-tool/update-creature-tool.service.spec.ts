import { TestBed } from '@angular/core/testing';

import { UpdateCreatureToolService } from './update-creature-tool.service';

describe('UpdateCreatureToolService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: UpdateCreatureToolService = TestBed.get(UpdateCreatureToolService);
    expect(service).toBeTruthy();
  });
});
