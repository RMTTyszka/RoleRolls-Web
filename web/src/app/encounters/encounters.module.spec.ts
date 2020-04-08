import {EncountersModule} from './encounters.module';

describe('EncountersModule', () => {
  let encountersModule: EncountersModule;

  beforeEach(() => {
    encountersModule = new EncountersModule();
  });

  it('should create an instance', () => {
    expect(encountersModule).toBeTruthy();
  });
});
