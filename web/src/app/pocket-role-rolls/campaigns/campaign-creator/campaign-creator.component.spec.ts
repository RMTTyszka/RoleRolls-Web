import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CampaignCreatorComponent } from './campaign-creator.component';

describe('CampaignCreatorComponent', () => {
  let component: CampaignCreatorComponent;
  let fixture: ComponentFixture<CampaignCreatorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CampaignCreatorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CampaignCreatorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
