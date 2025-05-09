import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdvantageSelectComponent } from './advantage-select.component';

describe('AdvantageSelectComponent', () => {
  let component: AdvantageSelectComponent;
  let fixture: ComponentFixture<AdvantageSelectComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdvantageSelectComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdvantageSelectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
