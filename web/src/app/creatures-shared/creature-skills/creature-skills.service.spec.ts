import { TestBed } from '@angular/core/testing';

import { CreatureSkillsService } from './creature-skills.service';

describe('CreatureSkillsService', () => {
  let service: CreatureSkillsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CreatureSkillsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
