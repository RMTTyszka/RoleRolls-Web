import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BonusToolComponent } from './bonus-tool.component';

describe('BonusToolComponent', () => {
  let component: BonusToolComponent;
  let fixture: ComponentFixture<BonusToolComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BonusToolComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BonusToolComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
