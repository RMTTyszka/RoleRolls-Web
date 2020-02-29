import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CmGridComponent } from './cm-grid.component';

describe('CbGridComponent', () => {
  let component: CmGridComponent;
  let fixture: ComponentFixture<CmGridComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CmGridComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CmGridComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
