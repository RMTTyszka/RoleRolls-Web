import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ItemConfigurationComponent } from './item-configuration.component';

describe('ItemConfigurationComponent', () => {
  let component: ItemConfigurationComponent;
  let fixture: ComponentFixture<ItemConfigurationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ItemConfigurationComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ItemConfigurationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
