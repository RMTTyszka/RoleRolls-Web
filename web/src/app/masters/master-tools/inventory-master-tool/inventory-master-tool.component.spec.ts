import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { InventoryMasterToolComponent } from './inventory-master-tool.component';

describe('InventoryMasterToolComponent', () => {
  let component: InventoryMasterToolComponent;
  let fixture: ComponentFixture<InventoryMasterToolComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ InventoryMasterToolComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(InventoryMasterToolComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
