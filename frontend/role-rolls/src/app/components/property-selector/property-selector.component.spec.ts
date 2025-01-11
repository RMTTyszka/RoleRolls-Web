import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PropertySelectorComponent } from './property-selector.component';

describe('PropertySelectorComponent', () => {
  let component: PropertySelectorComponent;
  let fixture: ComponentFixture<PropertySelectorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PropertySelectorComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PropertySelectorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
