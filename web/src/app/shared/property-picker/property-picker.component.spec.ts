import {async, ComponentFixture, TestBed} from '@angular/core/testing';

import {PropertyPickerComponent} from './property-picker.component';

describe('PropertyPickerComponent', () => {
  let component: PropertyPickerComponent;
  let fixture: ComponentFixture<PropertyPickerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PropertyPickerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PropertyPickerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
