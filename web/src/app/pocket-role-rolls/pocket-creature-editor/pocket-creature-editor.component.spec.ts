import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PocketCreatureEditorComponent } from './pocket-creature-editor.component';

describe('PocketCreatureEditorComponent', () => {
  let component: PocketCreatureEditorComponent;
  let fixture: ComponentFixture<PocketCreatureEditorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PocketCreatureEditorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PocketCreatureEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
