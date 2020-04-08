import {RolesSharedModule} from './roles-shared.module';

describe('RolesSharedModule', () => {
  let rolesSharedModule: RolesSharedModule;

  beforeEach(() => {
    rolesSharedModule = new RolesSharedModule();
  });

  it('should create an instance', () => {
    expect(rolesSharedModule).toBeTruthy();
  });
});
