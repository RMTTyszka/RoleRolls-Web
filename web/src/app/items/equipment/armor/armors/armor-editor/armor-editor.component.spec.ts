import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ArmorEditorComponent } from './armor-editor.component';

describe('ArmorEditorComponent', () => {
  let component: ArmorEditorComponent;
  let fixture: ComponentFixture<ArmorEditorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ArmorEditorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ArmorEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
