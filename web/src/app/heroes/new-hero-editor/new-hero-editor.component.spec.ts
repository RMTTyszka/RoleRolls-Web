import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NewHeroEditorComponent } from './new-hero-editor.component';

describe('NewHeroEditorComponent', () => {
  let component: NewHeroEditorComponent;
  let fixture: ComponentFixture<NewHeroEditorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NewHeroEditorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NewHeroEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
