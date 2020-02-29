import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PowerManagementComponent } from './power-management.component';

describe('PowerManagementComponent', () => {
  let component: PowerManagementComponent;
  let fixture: ComponentFixture<PowerManagementComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PowerManagementComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PowerManagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
