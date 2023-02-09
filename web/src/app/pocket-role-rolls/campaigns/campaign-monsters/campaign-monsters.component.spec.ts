import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CampaignMonstersComponent } from './campaign-monsters.component';

describe('CampaignMonstersComponent', () => {
  let component: CampaignMonstersComponent;
  let fixture: ComponentFixture<CampaignMonstersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CampaignMonstersComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CampaignMonstersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
