import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RrSelectModalComponent } from './rr-select-modal.component';

describe('RrSelectModalComponent', () => {
  let component: RrSelectModalComponent;
  let fixture: ComponentFixture<RrSelectModalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RrSelectModalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RrSelectModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
