import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateCreatureToolComponent } from './update-creature-tool.component';

describe('UpdateCreatureToolComponent', () => {
  let component: UpdateCreatureToolComponent;
  let fixture: ComponentFixture<UpdateCreatureToolComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UpdateCreatureToolComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UpdateCreatureToolComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
