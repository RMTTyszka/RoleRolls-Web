import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreatureTypeEditorComponent } from 'app/campaigns/campaign-details/creature-types/creature-type-editor/creature-type-editor.component';

describe('CreatureTypeEditorComponent', () => {
  let component: CreatureTypeEditorComponent;
  let fixture: ComponentFixture<CreatureTypeEditorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CreatureTypeEditorComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreatureTypeEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
