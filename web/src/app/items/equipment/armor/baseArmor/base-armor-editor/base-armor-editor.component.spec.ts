import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BaseArmorEditorComponent } from './base-armor-editor.component';

describe('BaseArmorEditorComponent', () => {
  let component: BaseArmorEditorComponent;
  let fixture: ComponentFixture<BaseArmorEditorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BaseArmorEditorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BaseArmorEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
