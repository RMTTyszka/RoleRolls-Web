import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VitalityManagerComponent } from './vitality-manager.component';

describe('VitalityManagerComponent', () => {
  let component: VitalityManagerComponent;
  let fixture: ComponentFixture<VitalityManagerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [VitalityManagerComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(VitalityManagerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
