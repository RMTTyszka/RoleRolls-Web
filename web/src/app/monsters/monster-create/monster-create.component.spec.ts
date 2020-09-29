import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MonsterCreateComponent } from './monster-create.component';

describe('MonsterCreateComponent', () => {
  let component: MonsterCreateComponent;
  let fixture: ComponentFixture<MonsterCreateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MonsterCreateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MonsterCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
