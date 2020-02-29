import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PowerSelectorComponent } from './power-selector.component';

describe('PowerSelectorComponent', () => {
  let component: PowerSelectorComponent;
  let fixture: ComponentFixture<PowerSelectorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PowerSelectorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PowerSelectorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
