import {async, ComponentFixture, TestBed} from '@angular/core/testing';

import {ArmorSelectorComponent} from './armor-selector.component';

describe('ArmorSelectorComponent', () => {
  let component: ArmorSelectorComponent;
  let fixture: ComponentFixture<ArmorSelectorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ArmorSelectorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ArmorSelectorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
