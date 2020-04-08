import {async, ComponentFixture, TestBed} from '@angular/core/testing';

import {RaceEditorComponent} from './race-editor.component';

describe('RaceEditorComponent', () => {
  let component: RaceEditorComponent;
  let fixture: ComponentFixture<RaceEditorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RaceEditorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RaceEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
