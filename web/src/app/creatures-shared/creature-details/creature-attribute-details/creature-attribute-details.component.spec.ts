import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreatureAttributeDetailsComponent } from './creature-attribute-details.component';

describe('CreatureAttributeDetailsComponent', () => {
  let component: CreatureAttributeDetailsComponent;
  let fixture: ComponentFixture<CreatureAttributeDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreatureAttributeDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreatureAttributeDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
