import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CampaignCreaturesComponent } from './campaign-creatures.component';

describe('CampaignCreaturesComponent', () => {
  let component: CampaignCreaturesComponent;
  let fixture: ComponentFixture<CampaignCreaturesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CampaignCreaturesComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CampaignCreaturesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
