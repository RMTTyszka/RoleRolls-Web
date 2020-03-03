import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AttributeSelectComponent } from './attribute-select.component';

describe('AttributeSelectComponent', () => {
  let component: AttributeSelectComponent;
  let fixture: ComponentFixture<AttributeSelectComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AttributeSelectComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AttributeSelectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
