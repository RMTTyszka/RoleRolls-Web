import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SceneRollsComponent } from './scene-rolls.component';

describe('SceneRollsComponent', () => {
  let component: SceneRollsComponent;
  let fixture: ComponentFixture<SceneRollsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SceneRollsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SceneRollsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
