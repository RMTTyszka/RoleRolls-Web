import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreatureEquipmentSlotComponent } from './creature-equipment-slot.component';

describe('CreatureEquipmentSlotComponent', () => {
  let component: CreatureEquipmentSlotComponent;
  let fixture: ComponentFixture<CreatureEquipmentSlotComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CreatureEquipmentSlotComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreatureEquipmentSlotComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
