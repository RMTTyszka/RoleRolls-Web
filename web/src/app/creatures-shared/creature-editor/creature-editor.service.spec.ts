import { TestBed } from '@angular/core/testing';

import { CreatureEditorService } from './creature-editor.service';

describe('CreatureEditorService', () => {
  let service: CreatureEditorService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CreatureEditorService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
