import {async, ComponentFixture, TestBed} from '@angular/core/testing';

import {InventoryNeckaccesorySelectComponent} from './inventory-neckaccesory-select.component';

describe('InventoryNeckaccesorySelectComponent', () => {
  let component: InventoryNeckaccesorySelectComponent;
  let fixture: ComponentFixture<InventoryNeckaccesorySelectComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ InventoryNeckaccesorySelectComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(InventoryNeckaccesorySelectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
