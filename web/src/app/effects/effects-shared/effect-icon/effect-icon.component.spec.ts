import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EffectIconComponent } from './effect-icon.component';

describe('EffectIconComponent', () => {
  let component: EffectIconComponent;
  let fixture: ComponentFixture<EffectIconComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EffectIconComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EffectIconComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
