import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreatureSelectComponent } from './creature-select.component';

describe('CreatureSelectComponent', () => {
  let component: CreatureSelectComponent;
  let fixture: ComponentFixture<CreatureSelectComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CreatureSelectComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreatureSelectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
