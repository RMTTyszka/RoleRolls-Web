import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreatureEditorComponent } from './creature-editor.component';

describe('CreatureEditorComponent', () => {
  let component: CreatureEditorComponent;
  let fixture: ComponentFixture<CreatureEditorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CreatureEditorComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreatureEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
