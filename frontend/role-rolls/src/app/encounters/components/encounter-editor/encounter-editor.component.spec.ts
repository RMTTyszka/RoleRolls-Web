import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EncounterEditorComponent } from './encounter-editor.component';

describe('EncounterEditorComponent', () => {
  let component: EncounterEditorComponent;
  let fixture: ComponentFixture<EncounterEditorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EncounterEditorComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EncounterEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
