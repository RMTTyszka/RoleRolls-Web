import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RolesSelectModalComponent } from './roles-select-modal.component';

describe('RolesSelectorComponent', () => {
  let component: RolesSelectModalComponent;
  let fixture: ComponentFixture<RolesSelectModalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RolesSelectModalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RolesSelectModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
