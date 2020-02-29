import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ArmorListComponent } from './armor-list.component';

describe('ArmorListComponent', () => {
  let component: ArmorListComponent;
  let fixture: ComponentFixture<ArmorListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ArmorListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ArmorListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
