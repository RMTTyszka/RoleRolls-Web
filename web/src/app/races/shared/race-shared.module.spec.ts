import {RaceSharedModule} from './race-shared.module';

describe('SharedModule', () => {
  let sharedModule: RaceSharedModule;

  beforeEach(() => {
    sharedModule = new RaceSharedModule();
  });

  it('should create an instance', () => {
    expect(sharedModule).toBeTruthy();
  });
});
