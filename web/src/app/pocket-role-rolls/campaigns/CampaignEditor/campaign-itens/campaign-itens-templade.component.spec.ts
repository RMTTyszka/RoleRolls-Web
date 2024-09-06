import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CampaignItensTempladeComponent } from 'src/app/pocket-role-rolls/campaigns/CampaignInstance/campaign-itens/campaign-itens-templade.component';

describe('CampaignItensComponent', () => {
  let component: CampaignItensTempladeComponent;
  let fixture: ComponentFixture<CampaignItensTempladeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CampaignItensTempladeComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CampaignItensTempladeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
