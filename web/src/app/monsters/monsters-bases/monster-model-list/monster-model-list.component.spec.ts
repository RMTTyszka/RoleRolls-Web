import {async, ComponentFixture, TestBed} from '@angular/core/testing';

import {MonsterBaseListComponent} from './monster-model-list.component';

describe('MonsterBaseListComponent', () => {
  let component: MonsterBaseListComponent;
  let fixture: ComponentFixture<MonsterBaseListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MonsterBaseListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MonsterBaseListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
