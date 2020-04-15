import {async, ComponentFixture, TestBed} from '@angular/core/testing';

import {CreatureResistancesComponent} from './creature-resistances.component';

describe('CreatureResistancesComponent', () => {
  let component: CreatureResistancesComponent;
  let fixture: ComponentFixture<CreatureResistancesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreatureResistancesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreatureResistancesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
