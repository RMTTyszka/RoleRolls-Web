import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { InitiativeComponent } from './initiative.component';

describe('InitiativeComponent', () => {
  let component: InitiativeComponent;
  let fixture: ComponentFixture<InitiativeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ InitiativeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(InitiativeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
