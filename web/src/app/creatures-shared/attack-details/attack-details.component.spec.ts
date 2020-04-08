import {async, ComponentFixture, TestBed} from '@angular/core/testing';

import {AttackDetailsComponent} from './attack-details.component';

describe('AttackDetailsComponent', () => {
  let component: AttackDetailsComponent;
  let fixture: ComponentFixture<AttackDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AttackDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AttackDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
