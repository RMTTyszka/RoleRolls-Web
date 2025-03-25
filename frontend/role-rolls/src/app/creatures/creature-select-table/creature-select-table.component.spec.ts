import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreatureSelectTableComponent } from './creature-select-table.component';

describe('CreatureSelectTableComponent', () => {
  let component: CreatureSelectTableComponent;
  let fixture: ComponentFixture<CreatureSelectTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CreatureSelectTableComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreatureSelectTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
