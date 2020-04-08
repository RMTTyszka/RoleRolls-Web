import {async, ComponentFixture, TestBed} from '@angular/core/testing';

import {CombatCreatureSelectComponent} from './combat-creature-select.component';

describe('CombatCreatureSelectComponent', () => {
  let component: CombatCreatureSelectComponent;
  let fixture: ComponentFixture<CombatCreatureSelectComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CombatCreatureSelectComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CombatCreatureSelectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
