import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CampaignDetailsComponent } from './campaign-details.component';

describe('CampaignDetailsComponent', () => {
  let component: CampaignDetailsComponent;
  let fixture: ComponentFixture<CampaignDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CampaignDetailsComponent]
    })
    .overrideComponent(CampaignDetailsComponent, {
      set: {
        template: ''
      }
    })
    .compileComponents();

    fixture = TestBed.createComponent(CampaignDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
