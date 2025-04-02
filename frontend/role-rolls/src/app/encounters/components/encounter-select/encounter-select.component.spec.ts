import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EncounterSelectComponent } from './encounter-select.component';

describe('EncounterSelectComponent', () => {
  let component: EncounterSelectComponent;
  let fixture: ComponentFixture<EncounterSelectComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EncounterSelectComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EncounterSelectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
