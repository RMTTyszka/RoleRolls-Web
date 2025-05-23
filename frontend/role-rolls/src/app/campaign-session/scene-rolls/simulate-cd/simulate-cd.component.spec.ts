import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SimulateCdComponent } from './simulate-cd.component';

describe('SimulateCdComponent', () => {
  let component: SimulateCdComponent;
  let fixture: ComponentFixture<SimulateCdComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SimulateCdComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SimulateCdComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
