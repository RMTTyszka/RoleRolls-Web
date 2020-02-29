import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NewHeroAddButtonComponent } from './new-hero-add-button.component';

describe('NewHeroAddButtonComponent', () => {
  let component: NewHeroAddButtonComponent;
  let fixture: ComponentFixture<NewHeroAddButtonComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NewHeroAddButtonComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NewHeroAddButtonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
