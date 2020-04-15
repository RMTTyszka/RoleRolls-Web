import {async, ComponentFixture, TestBed} from '@angular/core/testing';

import {InventoryBeltSelectComponent} from './inventory-belt-select.component';

describe('InventoryBeltSelectComponent', () => {
  let component: InventoryBeltSelectComponent;
  let fixture: ComponentFixture<InventoryBeltSelectComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ InventoryBeltSelectComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(InventoryBeltSelectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
