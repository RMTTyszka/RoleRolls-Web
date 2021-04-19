import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ItemTemplateSelectComponent } from './item-template-select.component';

describe('ItemTemplateSelectModalComponent', () => {
  let component: ItemTemplateSelectComponent;
  let fixture: ComponentFixture<ItemTemplateSelectComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ItemTemplateSelectComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ItemTemplateSelectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
