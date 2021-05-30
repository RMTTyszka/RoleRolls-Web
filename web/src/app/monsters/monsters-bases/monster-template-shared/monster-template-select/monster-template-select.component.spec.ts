import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MonsterTemplateSelectComponent } from './monster-template-select.component';

describe('MonsterTemplateSelectComponent', () => {
  let component: MonsterTemplateSelectComponent;
  let fixture: ComponentFixture<MonsterTemplateSelectComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MonsterTemplateSelectComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MonsterTemplateSelectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
