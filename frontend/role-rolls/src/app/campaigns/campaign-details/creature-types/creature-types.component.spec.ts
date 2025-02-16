import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreatureTypesComponent } from './creature-types.component';

describe('CreatureTypesComponent', () => {
  let component: CreatureTypesComponent;
  let fixture: ComponentFixture<CreatureTypesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CreatureTypesComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreatureTypesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
