import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateHeroWeaponSelectComponent } from './create-hero-weapon-select.component';

describe('CreateHeroWeaponSelectComponent', () => {
  let component: CreateHeroWeaponSelectComponent;
  let fixture: ComponentFixture<CreateHeroWeaponSelectComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateHeroWeaponSelectComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateHeroWeaponSelectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
