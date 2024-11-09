import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ItemInstantiatorComponent } from './item-instantiator.component';

describe('ItemInstantiatorComponent', () => {
  let component: ItemInstantiatorComponent;
  let fixture: ComponentFixture<ItemInstantiatorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ItemInstantiatorComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ItemInstantiatorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
