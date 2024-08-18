import { TestBed } from '@angular/core/testing';

import { SceneNotificationService } from './scene-notification.service';

describe('SceneNotificationService', () => {
  let service: SceneNotificationService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SceneNotificationService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
