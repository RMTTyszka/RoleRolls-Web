import {async, ComponentFixture, TestBed} from '@angular/core/testing';

import {WeaponModelEditorComponent} from './weapon-model-editor.component';

describe('WeaponModelEditorComponent', () => {
  let component: WeaponModelEditorComponent;
  let fixture: ComponentFixture<WeaponModelEditorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ WeaponModelEditorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WeaponModelEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
