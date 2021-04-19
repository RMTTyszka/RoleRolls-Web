import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MasterItemInstantiatorComponent } from './master-item-instantiator.component';

describe('MasterItemInstantiatorComponent', () => {
  let component: MasterItemInstantiatorComponent;
  let fixture: ComponentFixture<MasterItemInstantiatorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MasterItemInstantiatorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MasterItemInstantiatorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
