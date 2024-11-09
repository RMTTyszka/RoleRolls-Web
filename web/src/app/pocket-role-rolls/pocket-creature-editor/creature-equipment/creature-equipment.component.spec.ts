import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreatureEquipmentComponent } from './creature-equipment.component';

describe('CreatureEquipmentComponent', () => {
  let component: CreatureEquipmentComponent;
  let fixture: ComponentFixture<CreatureEquipmentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CreatureEquipmentComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreatureEquipmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
