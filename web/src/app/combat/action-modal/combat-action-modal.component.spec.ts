import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CombatActionModalComponent } from './combat-action-modal.component';

describe('ActionModalComponent', () => {
  let component: CombatActionModalComponent;
  let fixture: ComponentFixture<CombatActionModalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CombatActionModalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CombatActionModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
