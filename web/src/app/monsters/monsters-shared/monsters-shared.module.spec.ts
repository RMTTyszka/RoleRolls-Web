import { MonstersSharedModule } from './monsters-shared.module';

describe('MonstersSharedModule', () => {
  let monstersSharedModule: MonstersSharedModule;

  beforeEach(() => {
    monstersSharedModule = new MonstersSharedModule();
  });

  it('should create an instance', () => {
    expect(monstersSharedModule).toBeTruthy();
  });
});
