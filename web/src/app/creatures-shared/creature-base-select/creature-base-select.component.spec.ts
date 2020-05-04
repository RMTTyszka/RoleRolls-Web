import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreatureBaseSelectComponent } from './creature-base-select.component';

describe('CreatureBaseSelectComponent', () => {
  let component: CreatureBaseSelectComponent;
  let fixture: ComponentFixture<CreatureBaseSelectComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreatureBaseSelectComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreatureBaseSelectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
