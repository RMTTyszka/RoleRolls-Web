import {async, ComponentFixture, TestBed} from '@angular/core/testing';

import {HeroesSelectModalComponent} from './heroes-select-modal.component';

describe('HeroesSelectModalComponent', () => {
  let component: HeroesSelectModalComponent;
  let fixture: ComponentFixture<HeroesSelectModalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HeroesSelectModalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HeroesSelectModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
