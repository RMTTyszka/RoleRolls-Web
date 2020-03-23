import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { InventoryHeadpieceSelectComponent } from './inventory-headpiece-select.component';

describe('InventoryHeadpieceSelectComponent', () => {
  let component: InventoryHeadpieceSelectComponent;
  let fixture: ComponentFixture<InventoryHeadpieceSelectComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ InventoryHeadpieceSelectComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(InventoryHeadpieceSelectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
