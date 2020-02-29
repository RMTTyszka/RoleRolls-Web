import { RollsModule } from './rolls.module';

describe('RollsModule', () => {
  let rollsModule: RollsModule;

  beforeEach(() => {
    rollsModule = new RollsModule();
  });

  it('should create an instance', () => {
    expect(rollsModule).toBeTruthy();
  });
});
