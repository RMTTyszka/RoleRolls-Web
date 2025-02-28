import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CampaignSessionComponent } from './campaign-session.component';

describe('CampaignSessionComponent', () => {
  let component: CampaignSessionComponent;
  let fixture: ComponentFixture<CampaignSessionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CampaignSessionComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CampaignSessionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
