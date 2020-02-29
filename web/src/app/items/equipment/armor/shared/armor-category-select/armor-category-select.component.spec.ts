import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ArmorCategorySelectComponent } from './armor-category-select.component';

describe('ArmorCategorySelectComponent', () => {
  let component: ArmorCategorySelectComponent;
  let fixture: ComponentFixture<ArmorCategorySelectComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ArmorCategorySelectComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ArmorCategorySelectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
