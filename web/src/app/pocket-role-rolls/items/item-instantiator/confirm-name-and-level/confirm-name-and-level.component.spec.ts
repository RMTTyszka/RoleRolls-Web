import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConfirmNameAndLevelComponent } from './confirm-name-and-level.component';

describe('ConfirmNameAndLevelComponent', () => {
  let component: ConfirmNameAndLevelComponent;
  let fixture: ComponentFixture<ConfirmNameAndLevelComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ConfirmNameAndLevelComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ConfirmNameAndLevelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
