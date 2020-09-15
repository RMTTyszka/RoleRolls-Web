import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreatureDetailsComponent } from './creature-details.component';

describe('CreatureDetailsComponent', () => {
  let component: CreatureDetailsComponent;
  let fixture: ComponentFixture<CreatureDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreatureDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreatureDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
