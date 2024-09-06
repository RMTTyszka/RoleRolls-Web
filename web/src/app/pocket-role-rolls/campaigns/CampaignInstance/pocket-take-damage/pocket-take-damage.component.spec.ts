import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PocketTakeDamageComponent } from './pocket-take-damage.component';

describe('PocketTakeDamageComponent', () => {
  let component: PocketTakeDamageComponent;
  let fixture: ComponentFixture<PocketTakeDamageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PocketTakeDamageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PocketTakeDamageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
