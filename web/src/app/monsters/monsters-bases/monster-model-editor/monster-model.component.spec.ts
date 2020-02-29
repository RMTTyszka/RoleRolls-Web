import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MonsterModelComponent } from './monster-model.component';

describe('MonsterModelComponent', () => {
  let component: MonsterModelComponent;
  let fixture: ComponentFixture<MonsterModelComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MonsterModelComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MonsterModelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
