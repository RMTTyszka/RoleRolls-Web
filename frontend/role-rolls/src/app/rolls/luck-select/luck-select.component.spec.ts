import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LuckSelectComponent } from './luck-select.component';

describe('LuckSelectComponent', () => {
  let component: LuckSelectComponent;
  let fixture: ComponentFixture<LuckSelectComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LuckSelectComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LuckSelectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
