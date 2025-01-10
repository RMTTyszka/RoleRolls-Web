import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CampaignItemConfigurationComponent } from './campaign-item-configuration.component';

describe('CampaignItemConfigurationComponent', () => {
  let component: CampaignItemConfigurationComponent;
  let fixture: ComponentFixture<CampaignItemConfigurationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CampaignItemConfigurationComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CampaignItemConfigurationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
