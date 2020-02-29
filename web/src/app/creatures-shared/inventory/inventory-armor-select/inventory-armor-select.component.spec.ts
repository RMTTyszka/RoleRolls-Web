import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { InventoryArmorSelectComponent } from './inventory-armor-select.component';

describe('InventoryArmorSelectComponent', () => {
  let component: InventoryArmorSelectComponent;
  let fixture: ComponentFixture<InventoryArmorSelectComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ InventoryArmorSelectComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(InventoryArmorSelectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
