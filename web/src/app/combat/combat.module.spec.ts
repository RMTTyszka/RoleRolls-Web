import { CombatModule } from './combat.module';

describe('CombatModule', () => {
  let combatModule: CombatModule;

  beforeEach(() => {
    combatModule = new CombatModule();
  });

  it('should create an instance', () => {
    expect(combatModule).toBeTruthy();
  });
});
