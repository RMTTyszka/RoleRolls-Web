import {async, ComponentFixture, TestBed} from '@angular/core/testing';

import {WeaponModelListComponent} from './weapon-model-list.component';

describe('WeaponModelListComponent', () => {
  let component: WeaponModelListComponent;
  let fixture: ComponentFixture<WeaponModelListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ WeaponModelListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WeaponModelListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
