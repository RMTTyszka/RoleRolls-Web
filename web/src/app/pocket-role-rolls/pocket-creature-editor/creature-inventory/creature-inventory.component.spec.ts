import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreatureInventoryComponent } from './creature-inventory.component';

describe('CreatureInventoryComponent', () => {
  let component: CreatureInventoryComponent;
  let fixture: ComponentFixture<CreatureInventoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CreatureInventoryComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreatureInventoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
