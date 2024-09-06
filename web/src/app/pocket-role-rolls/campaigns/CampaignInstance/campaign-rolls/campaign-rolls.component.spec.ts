import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CampaignRollsComponent } from './campaign-rolls.component';

describe('CampaignRollsComponent', () => {
  let component: CampaignRollsComponent;
  let fixture: ComponentFixture<CampaignRollsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CampaignRollsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CampaignRollsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
