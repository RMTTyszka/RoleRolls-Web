import { TestBed } from '@angular/core/testing';
import { ResolveFn } from '@angular/router';
import { Campaign } from '../../models/campaign';

import { campaignResolver } from './campaign.resolver';

describe('campaignResolver', () => {
  const executeResolver: ResolveFn<Campaign> = (...resolverParameters) =>
      TestBed.runInInjectionContext(() => campaignResolver(...resolverParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeResolver).toBeTruthy();
  });
});
