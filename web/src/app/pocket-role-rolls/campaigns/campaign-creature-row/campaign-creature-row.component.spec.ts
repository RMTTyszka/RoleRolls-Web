import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CampaignCreatureRowComponent } from './campaign-creature-row.component';

describe('CampaignCreatureRowComponent', () => {
  let component: CampaignCreatureRowComponent;
  let fixture: ComponentFixture<CampaignCreatureRowComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CampaignCreatureRowComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CampaignCreatureRowComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
