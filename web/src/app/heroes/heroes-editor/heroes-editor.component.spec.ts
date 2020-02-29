import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HeroesEditorComponent } from './heroes-editor.component';

describe('HeroesEditorComponent', () => {
  let component: HeroesEditorComponent;
  let fixture: ComponentFixture<HeroesEditorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HeroesEditorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HeroesEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
