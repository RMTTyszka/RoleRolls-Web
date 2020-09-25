import {async, ComponentFixture, TestBed} from '@angular/core/testing';

import {RrGridComponent} from './rr-grid.component';

describe('CbGridComponent', () => {
  let component: RrGridComponent;
  let fixture: ComponentFixture<RrGridComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RrGridComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RrGridComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
