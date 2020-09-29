import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CampaignHeroesComponent } from './campaign-heroes.component';

describe('CampaignHeroesComponent', () => {
  let component: CampaignHeroesComponent;
  let fixture: ComponentFixture<CampaignHeroesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CampaignHeroesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CampaignHeroesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
