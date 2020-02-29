import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MonsterBaseSelectorComponent } from './monster-model-selector.component';

describe('MonsterBaseSelectorComponent', () => {
  let component: MonsterBaseSelectorComponent;
  let fixture: ComponentFixture<MonsterBaseSelectorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MonsterBaseSelectorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MonsterBaseSelectorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
