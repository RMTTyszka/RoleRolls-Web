import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CampaignEditorBodyShellComponent } from './campaign-editor-body-shell.component';

describe('CampaignEditorBodyShellComponent', () => {
  let component: CampaignEditorBodyShellComponent;
  let fixture: ComponentFixture<CampaignEditorBodyShellComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CampaignEditorBodyShellComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CampaignEditorBodyShellComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
