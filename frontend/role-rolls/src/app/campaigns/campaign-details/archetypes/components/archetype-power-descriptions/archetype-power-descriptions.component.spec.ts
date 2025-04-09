import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ArchetypePowerDescriptionsComponent } from './archetype-power-descriptions.component';

describe('ArchetypePowerDescriptionsComponent', () => {
  let component: ArchetypePowerDescriptionsComponent;
  let fixture: ComponentFixture<ArchetypePowerDescriptionsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ArchetypePowerDescriptionsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ArchetypePowerDescriptionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
