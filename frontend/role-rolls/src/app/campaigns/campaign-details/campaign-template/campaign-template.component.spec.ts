import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CampaignTemplateComponent } from './campaign-template.component';

describe('CampaignTemplateComponent', () => {
  let component: CampaignTemplateComponent;
  let fixture: ComponentFixture<CampaignTemplateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CampaignTemplateComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CampaignTemplateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
