import {async, ComponentFixture, TestBed} from '@angular/core/testing';

import {RaceModalSelectorComponent} from './race-modal-selector.component';

describe('RaceSelectorComponent', () => {
  let component: RaceModalSelectorComponent;
  let fixture: ComponentFixture<RaceModalSelectorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RaceModalSelectorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RaceModalSelectorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
