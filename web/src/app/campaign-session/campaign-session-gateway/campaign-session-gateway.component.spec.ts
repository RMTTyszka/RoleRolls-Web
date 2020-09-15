import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CampaignSessionGatewayComponent } from './campaign-session-gateway.component';

describe('CampaignSessionGatewayComponent', () => {
  let component: CampaignSessionGatewayComponent;
  let fixture: ComponentFixture<CampaignSessionGatewayComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CampaignSessionGatewayComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CampaignSessionGatewayComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
