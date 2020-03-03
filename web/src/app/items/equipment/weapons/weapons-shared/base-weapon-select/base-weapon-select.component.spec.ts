import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BaseWeaponSelectComponent } from './base-weapon-select.component';

describe('BaseWeaponSelectComponent', () => {
  let component: BaseWeaponSelectComponent;
  let fixture: ComponentFixture<BaseWeaponSelectComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BaseWeaponSelectComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BaseWeaponSelectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
