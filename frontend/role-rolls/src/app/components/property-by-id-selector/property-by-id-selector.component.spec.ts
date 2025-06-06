import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PropertyByIdSelectorComponent } from './property-by-id-selector.component';

describe('PropertyByIdSelectorComponent', () => {
  let component: PropertyByIdSelectorComponent;
  let fixture: ComponentFixture<PropertyByIdSelectorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PropertyByIdSelectorComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PropertyByIdSelectorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
