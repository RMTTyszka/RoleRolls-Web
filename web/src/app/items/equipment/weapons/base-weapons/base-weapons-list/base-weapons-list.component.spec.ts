import {async, ComponentFixture, TestBed} from '@angular/core/testing';

import {BaseWeaponsListComponent} from './base-weapons-list.component';

describe('BaseWeaponsListComponent', () => {
  let component: BaseWeaponsListComponent;
  let fixture: ComponentFixture<BaseWeaponsListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BaseWeaponsListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BaseWeaponsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
