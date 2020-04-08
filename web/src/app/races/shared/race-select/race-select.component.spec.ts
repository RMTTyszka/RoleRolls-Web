import {async, ComponentFixture, TestBed} from '@angular/core/testing';

import {RaceSelectComponent} from './race-select.component';

describe('RaceSelectComponent', () => {
  let component: RaceSelectComponent;
  let fixture: ComponentFixture<RaceSelectComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RaceSelectComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RaceSelectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
