import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HeroSelectComponent } from './hero-select.component';

describe('HeroSelectComponent', () => {
  let component: HeroSelectComponent;
  let fixture: ComponentFixture<HeroSelectComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HeroSelectComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HeroSelectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
