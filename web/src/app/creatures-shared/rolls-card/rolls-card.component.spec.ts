import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RollsCardComponent } from './rolls-card.component';

describe('RollsCardComponent', () => {
  let component: RollsCardComponent;
  let fixture: ComponentFixture<RollsCardComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RollsCardComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RollsCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
