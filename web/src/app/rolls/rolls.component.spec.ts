import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RollsComponent } from './rolls.component';

describe('RollsComponent', () => {
  let component: RollsComponent;
  let fixture: ComponentFixture<RollsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RollsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RollsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
