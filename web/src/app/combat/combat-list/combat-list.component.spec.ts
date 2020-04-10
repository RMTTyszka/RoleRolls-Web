import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CombatListComponent } from './combat-list.component';

describe('CombatListComponent', () => {
  let component: CombatListComponent;
  let fixture: ComponentFixture<CombatListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CombatListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CombatListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
