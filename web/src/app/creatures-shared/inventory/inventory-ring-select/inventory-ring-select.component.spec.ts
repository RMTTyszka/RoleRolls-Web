import {async, ComponentFixture, TestBed} from '@angular/core/testing';

import {InventoryRingSelectComponent} from './inventory-ring-select.component';

describe('InventoryRingSelectComponent', () => {
  let component: InventoryRingSelectComponent;
  let fixture: ComponentFixture<InventoryRingSelectComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ InventoryRingSelectComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(InventoryRingSelectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
