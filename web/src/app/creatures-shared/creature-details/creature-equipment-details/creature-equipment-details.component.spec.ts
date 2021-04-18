import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreatureEquipmentDetailsComponent } from './creature-equipment-details.component';

describe('CreatureEquipmentDetailsComponent', () => {
  let component: CreatureEquipmentDetailsComponent;
  let fixture: ComponentFixture<CreatureEquipmentDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreatureEquipmentDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreatureEquipmentDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
