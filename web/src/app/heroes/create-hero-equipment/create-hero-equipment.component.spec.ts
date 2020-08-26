import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateHeroEquipmentComponent } from './create-hero-equipment.component';

describe('CreateHeroEquipmentComponent', () => {
  let component: CreateHeroEquipmentComponent;
  let fixture: ComponentFixture<CreateHeroEquipmentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateHeroEquipmentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateHeroEquipmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
