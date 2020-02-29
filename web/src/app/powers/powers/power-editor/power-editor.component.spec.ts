import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PowerEditorComponent } from './power-editor.component';

describe('PowerEditorComponent', () => {
  let component: PowerEditorComponent;
  let fixture: ComponentFixture<PowerEditorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PowerEditorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PowerEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
