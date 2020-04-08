import {async, ComponentFixture, TestBed} from '@angular/core/testing';

import {InventoryGlovesSelectComponent} from './inventory-gloves-select.component';

describe('InventoryGloveSelectComponent', () => {
  let component: InventoryGlovesSelectComponent;
  let fixture: ComponentFixture<InventoryGlovesSelectComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ InventoryGlovesSelectComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(InventoryGlovesSelectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
