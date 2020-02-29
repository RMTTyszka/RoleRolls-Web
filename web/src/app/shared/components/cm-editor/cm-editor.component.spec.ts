import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CmEditorComponent } from './cm-editor.component';

describe('CmEditorComponent', () => {
  let component: CmEditorComponent;
  let fixture: ComponentFixture<CmEditorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CmEditorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CmEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
