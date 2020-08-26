import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateHeroArmorSelectComponentComponent } from './create-hero-armor-select-component.component';

describe('CreateHeroArmorSelectComponentComponent', () => {
  let component: CreateHeroArmorSelectComponentComponent;
  let fixture: ComponentFixture<CreateHeroArmorSelectComponentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateHeroArmorSelectComponentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateHeroArmorSelectComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
