import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ItemTemplateSelectModalComponent } from './item-template-select-modal.component';

describe('ItemTemplateSelectModalComponent', () => {
  let component: ItemTemplateSelectModalComponent;
  let fixture: ComponentFixture<ItemTemplateSelectModalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ItemTemplateSelectModalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ItemTemplateSelectModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
