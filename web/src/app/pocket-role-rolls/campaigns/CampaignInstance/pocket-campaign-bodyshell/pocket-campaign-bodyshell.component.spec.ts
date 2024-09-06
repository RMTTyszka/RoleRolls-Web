import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PocketCampaignBodyshellComponent } from './pocket-campaign-bodyshell.component';

describe('PocketCampaignBodyshellComponent', () => {
  let component: PocketCampaignBodyshellComponent;
  let fixture: ComponentFixture<PocketCampaignBodyshellComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PocketCampaignBodyshellComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PocketCampaignBodyshellComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
