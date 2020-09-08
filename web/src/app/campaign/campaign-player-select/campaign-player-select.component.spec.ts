import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CampaignPlayerSelectComponent } from './campaign-player-select.component';

describe('CampaignPlayerSelectComponent', () => {
  let component: CampaignPlayerSelectComponent;
  let fixture: ComponentFixture<CampaignPlayerSelectComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CampaignPlayerSelectComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CampaignPlayerSelectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
