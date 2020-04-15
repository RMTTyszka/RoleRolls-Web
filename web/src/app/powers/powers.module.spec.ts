import {PowersModule} from './powers.module';

describe('PowersModule', () => {
  let powersModule: PowersModule;

  beforeEach(() => {
    powersModule = new PowersModule();
  });

  it('should create an instance', () => {
    expect(powersModule).toBeTruthy();
  });
});
