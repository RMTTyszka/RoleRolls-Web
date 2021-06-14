import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CampaignEncountersComponent } from './campaign-encounters.component';

describe('CampaignEncountersComponent', () => {
  let component: CampaignEncountersComponent;
  let fixture: ComponentFixture<CampaignEncountersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CampaignEncountersComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CampaignEncountersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
