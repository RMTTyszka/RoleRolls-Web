import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SceneLogComponent } from './scene-log.component';

describe('SceneLogComponent', () => {
  let component: SceneLogComponent;
  let fixture: ComponentFixture<SceneLogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SceneLogComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SceneLogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
