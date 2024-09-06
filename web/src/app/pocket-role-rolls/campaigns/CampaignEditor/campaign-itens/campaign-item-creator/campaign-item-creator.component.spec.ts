import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CampaignItemCreatorComponent } from './campaign-item-creator.component';

describe('CampaignItemCreatorComponent', () => {
  let component: CampaignItemCreatorComponent;
  let fixture: ComponentFixture<CampaignItemCreatorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CampaignItemCreatorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CampaignItemCreatorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
