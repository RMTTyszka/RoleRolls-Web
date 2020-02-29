import { PlacesModule } from './places.module';

describe('PlacesModule', () => {
  let placesModule: PlacesModule;

  beforeEach(() => {
    placesModule = new PlacesModule();
  });

  it('should create an instance', () => {
    expect(placesModule).toBeTruthy();
  });
});
