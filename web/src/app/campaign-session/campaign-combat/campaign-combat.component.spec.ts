import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CampaignCombatComponent } from './campaign-combat.component';

describe('CampaignCombatComponent', () => {
  let component: CampaignCombatComponent;
  let fixture: ComponentFixture<CampaignCombatComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CampaignCombatComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CampaignCombatComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
