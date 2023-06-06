import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CdDiscoveryComponent } from './cd-discovery.component';

describe('CdDiscoveryComponent', () => {
  let component: CdDiscoveryComponent;
  let fixture: ComponentFixture<CdDiscoveryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CdDiscoveryComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CdDiscoveryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
