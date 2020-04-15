import {async, ComponentFixture, TestBed} from '@angular/core/testing';

import {WeaponCategorySelectComponent} from './weapon-category-select.component';

describe('WeaponCategorySelectComponent', () => {
  let component: WeaponCategorySelectComponent;
  let fixture: ComponentFixture<WeaponCategorySelectComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ WeaponCategorySelectComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WeaponCategorySelectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
