import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RrSelectFieldComponent } from './rr-select-field.component';

describe('RrSelectFieldComponent', () => {
  let component: RrSelectFieldComponent;
  let fixture: ComponentFixture<RrSelectFieldComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RrSelectFieldComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RrSelectFieldComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
