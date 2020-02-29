import { TestsModule } from './tests.module';

describe('TestsModule', () => {
  let testsModule: TestsModule;

  beforeEach(() => {
    testsModule = new TestsModule();
  });

  it('should create an instance', () => {
    expect(testsModule).toBeTruthy();
  });
});
