import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CombatLogComponent } from './combat-log.component';

describe('CombatLogComponent', () => {
  let component: CombatLogComponent;
  let fixture: ComponentFixture<CombatLogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CombatLogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CombatLogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
