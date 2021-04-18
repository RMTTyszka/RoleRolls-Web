import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreatureInventoryDetailsComponent } from './creature-inventory-details.component';

describe('CreatureInventoryDetailsComponent', () => {
  let component: CreatureInventoryDetailsComponent;
  let fixture: ComponentFixture<CreatureInventoryDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreatureInventoryDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreatureInventoryDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
