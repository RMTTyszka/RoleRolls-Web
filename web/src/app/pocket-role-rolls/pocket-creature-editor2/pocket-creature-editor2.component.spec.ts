import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PocketCreatureEditor2Component } from './pocket-creature-editor2.component';

describe('PocketCreatureEditor2Component', () => {
  let component: PocketCreatureEditor2Component;
  let fixture: ComponentFixture<PocketCreatureEditor2Component>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PocketCreatureEditor2Component ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PocketCreatureEditor2Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
