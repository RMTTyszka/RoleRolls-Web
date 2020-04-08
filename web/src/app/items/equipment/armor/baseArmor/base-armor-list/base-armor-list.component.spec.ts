import {async, ComponentFixture, TestBed} from '@angular/core/testing';

import {BaseArmorListComponent} from './base-armor-list.component';

describe('BaseArmorListComponent', () => {
  let component: BaseArmorListComponent;
  let fixture: ComponentFixture<BaseArmorListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BaseArmorListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BaseArmorListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
