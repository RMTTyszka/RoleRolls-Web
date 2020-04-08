import {async, ComponentFixture, TestBed} from '@angular/core/testing';

import {HeroesGatewayComponent} from './heroes-gateway.component';

describe('HeroesGatewayComponent', () => {
  let component: HeroesGatewayComponent;
  let fixture: ComponentFixture<HeroesGatewayComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HeroesGatewayComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HeroesGatewayComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
