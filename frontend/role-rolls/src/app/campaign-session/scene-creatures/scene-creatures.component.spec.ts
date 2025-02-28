import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SceneCreaturesComponent } from './scene-creatures.component';

describe('SceneCreaturesComponent', () => {
  let component: SceneCreaturesComponent;
  let fixture: ComponentFixture<SceneCreaturesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SceneCreaturesComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SceneCreaturesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
