import {async, ComponentFixture, TestBed} from '@angular/core/testing';

import {BaseWeaponsEditorComponent} from './base-weapons-editor.component';

describe('BaseWeaponsEditorComponent', () => {
  let component: BaseWeaponsEditorComponent;
  let fixture: ComponentFixture<BaseWeaponsEditorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BaseWeaponsEditorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BaseWeaponsEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
