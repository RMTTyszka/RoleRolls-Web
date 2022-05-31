import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PocketLoginComponent } from './pocket-login.component';

describe('PocketLoginComponent', () => {
  let component: PocketLoginComponent;
  let fixture: ComponentFixture<PocketLoginComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PocketLoginComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PocketLoginComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
