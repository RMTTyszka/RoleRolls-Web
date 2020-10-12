import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MonsterShopComponent } from './monster-shop.component';

describe('MonsterShopComponent', () => {
  let component: MonsterShopComponent;
  let fixture: ComponentFixture<MonsterShopComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MonsterShopComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MonsterShopComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
