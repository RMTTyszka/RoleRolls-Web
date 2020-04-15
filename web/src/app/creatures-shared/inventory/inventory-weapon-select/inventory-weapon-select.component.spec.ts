import {async, ComponentFixture, TestBed} from '@angular/core/testing';

import {InventoryWeaponSelectComponent} from './inventory-weapon-select.component';

describe('InventoryWeaponSelectComponent', () => {
  let component: InventoryWeaponSelectComponent;
  let fixture: ComponentFixture<InventoryWeaponSelectComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ InventoryWeaponSelectComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(InventoryWeaponSelectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
