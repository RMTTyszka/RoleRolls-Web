import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PocketHomeComponent } from './pocket-home.component';

describe('PocketHomeComponent', () => {
  let component: PocketHomeComponent;
  let fixture: ComponentFixture<PocketHomeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PocketHomeComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PocketHomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
