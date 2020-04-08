import {async, ComponentFixture, TestBed} from '@angular/core/testing';

import {BaseArmorSelectorComponent} from './base-armor-selector.component';

describe('BaseArmorSelectorComponent', () => {
  let component: BaseArmorSelectorComponent;
  let fixture: ComponentFixture<BaseArmorSelectorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BaseArmorSelectorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BaseArmorSelectorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
