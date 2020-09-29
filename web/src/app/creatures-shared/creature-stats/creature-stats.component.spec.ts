import {async, ComponentFixture, TestBed} from '@angular/core/testing';

import {CreatureStatsComponent} from './creature-stats.component';

describe('HeroStatsComponent', () => {
  let component: CreatureStatsComponent;
  let fixture: ComponentFixture<CreatureStatsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreatureStatsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreatureStatsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
