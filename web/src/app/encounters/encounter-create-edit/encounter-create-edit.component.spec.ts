import {async, ComponentFixture, TestBed} from '@angular/core/testing';

import {EncounterCreateEditComponent} from './encounter-create-edit.component';

describe('EncounterCreateEditComponent', () => {
  let component: EncounterCreateEditComponent;
  let fixture: ComponentFixture<EncounterCreateEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EncounterCreateEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EncounterCreateEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
