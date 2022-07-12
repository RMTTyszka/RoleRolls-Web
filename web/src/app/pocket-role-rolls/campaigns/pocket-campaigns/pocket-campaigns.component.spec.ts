import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PocketCampaignsComponent } from './pocket-campaigns.component';

describe('PocketCampaignsComponent', () => {
  let component: PocketCampaignsComponent;
  let fixture: ComponentFixture<PocketCampaignsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PocketCampaignsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PocketCampaignsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
