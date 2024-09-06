import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PocketCreatureSelectComponent } from './pocket-creature-select.component';

describe('PocketCreatureSelectComponent', () => {
  let component: PocketCreatureSelectComponent;
  let fixture: ComponentFixture<PocketCreatureSelectComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PocketCreatureSelectComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PocketCreatureSelectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
