import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CampaignItensComponent } from './campaign-itens.component';

describe('CampaignItensComponent', () => {
  let component: CampaignItensComponent;
  let fixture: ComponentFixture<CampaignItensComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CampaignItensComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CampaignItensComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
