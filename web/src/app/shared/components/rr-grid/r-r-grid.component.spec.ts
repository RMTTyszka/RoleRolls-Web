import {async, ComponentFixture, TestBed} from '@angular/core/testing';

import {RRGridComponent} from './r-r-grid.component';

describe('CbGridComponent', () => {
  let component: RRGridComponent;
  let fixture: ComponentFixture<RRGridComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RRGridComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RRGridComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
