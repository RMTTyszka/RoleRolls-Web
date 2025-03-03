import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConfirmNameAndLevelItemComponent } from './confirm-name-and-level-item.component';

describe('ConfirmNameAndLevelItemComponent', () => {
  let component: ConfirmNameAndLevelItemComponent;
  let fixture: ComponentFixture<ConfirmNameAndLevelItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ConfirmNameAndLevelItemComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ConfirmNameAndLevelItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
