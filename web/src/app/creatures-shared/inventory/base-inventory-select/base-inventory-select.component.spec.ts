import {async, ComponentFixture, TestBed} from '@angular/core/testing';

import {BaseInventorySelectComponent} from './base-inventory-select.component';

describe('BaseInventorySelectComponent', () => {
  let component: BaseInventorySelectComponent;
  let fixture: ComponentFixture<BaseInventorySelectComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BaseInventorySelectComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BaseInventorySelectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
