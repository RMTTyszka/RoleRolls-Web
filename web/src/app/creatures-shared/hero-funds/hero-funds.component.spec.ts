import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HeroFundsComponent } from './hero-funds.component';

describe('HeroFundsComponent', () => {
  let component: HeroFundsComponent;
  let fixture: ComponentFixture<HeroFundsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HeroFundsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HeroFundsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
