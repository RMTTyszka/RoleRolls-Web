import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreatureEditorComponent } from './creature-editor.component';

describe('CreatureEditorComponent', () => {
  let component: CreatureEditorComponent;
  let fixture: ComponentFixture<CreatureEditorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreatureEditorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreatureEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
