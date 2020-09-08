import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PlayerSelectModalComponent } from './player-select-modal.component';

describe('PlayerSelectModalComponent', () => {
  let component: PlayerSelectModalComponent;
  let fixture: ComponentFixture<PlayerSelectModalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PlayerSelectModalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PlayerSelectModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
