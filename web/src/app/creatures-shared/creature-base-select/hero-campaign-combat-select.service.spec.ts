import { TestBed } from '@angular/core/testing';

import { CampaignCombatHeroService } from './campaign-combat-hero.service';

describe('HeroCampaignCombatSelectService', () => {
  let service: CampaignCombatHeroService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CampaignCombatHeroService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
