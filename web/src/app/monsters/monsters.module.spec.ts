import { MonstersModule } from './monsters.module';

describe('MonstersModule', () => {
  let monstersModule: MonstersModule;

  beforeEach(() => {
    monstersModule = new MonstersModule();
  });

  it('should create an instance', () => {
    expect(monstersModule).toBeTruthy();
  });
});
