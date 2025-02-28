import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SceneCreatureRowComponent } from './scene-creature-row.component';

describe('SceneCreatureRowComponent', () => {
  let component: SceneCreatureRowComponent;
  let fixture: ComponentFixture<SceneCreatureRowComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SceneCreatureRowComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SceneCreatureRowComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
