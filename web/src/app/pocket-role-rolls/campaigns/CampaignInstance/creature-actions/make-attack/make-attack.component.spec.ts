import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MakeAttackComponent } from './make-attack.component';

describe('MakeAttackComponent', () => {
  let component: MakeAttackComponent;
  let fixture: ComponentFixture<MakeAttackComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MakeAttackComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MakeAttackComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
