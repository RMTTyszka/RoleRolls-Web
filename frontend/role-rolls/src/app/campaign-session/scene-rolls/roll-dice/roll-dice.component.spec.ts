import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RollDiceComponent } from './roll-dice.component';

describe('RollDiceComponent', () => {
  let component: RollDiceComponent;
  let fixture: ComponentFixture<RollDiceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RollDiceComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RollDiceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
