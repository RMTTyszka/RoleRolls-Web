import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditorModalComponent } from './editor-modal.component';

describe('EditorModalComponent', () => {
  let component: EditorModalComponent;
  let fixture: ComponentFixture<EditorModalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditorModalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditorModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
