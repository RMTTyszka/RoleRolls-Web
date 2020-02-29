import { RacesModule } from './races.module';

describe('RacesModule', () => {
  let racesModule: RacesModule;

  beforeEach(() => {
    racesModule = new RacesModule();
  });

  it('should create an instance', () => {
    expect(racesModule).toBeTruthy();
  });
});
