import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CampaignHeroDetailsComponent } from './campaign-hero-details.component';

describe('CampaignHeroDetailsComponent', () => {
  let component: CampaignHeroDetailsComponent;
  let fixture: ComponentFixture<CampaignHeroDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CampaignHeroDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CampaignHeroDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
