import {async, ComponentFixture, TestBed} from '@angular/core/testing';

import {WeaponsTestComponent} from './weapons-test.component';

describe('WeaponsTestComponent', () => {
  let component: WeaponsTestComponent;
  let fixture: ComponentFixture<WeaponsTestComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ WeaponsTestComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WeaponsTestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
