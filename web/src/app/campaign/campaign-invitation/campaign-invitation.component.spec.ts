import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CampaignInvitationComponent } from './campaign-invitation.component';

describe('CampaignInvitationComponent', () => {
  let component: CampaignInvitationComponent;
  let fixture: ComponentFixture<CampaignInvitationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CampaignInvitationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CampaignInvitationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
