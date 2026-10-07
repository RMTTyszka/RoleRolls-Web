import { ComponentFixture, TestBed } from '@angular/core/testing';
import { DialogService } from 'primeng/dynamicdialog';
import { Router } from '@angular/router';
import { Entity } from '@app/models/Entity.model';

import { GridComponent } from './grid.component';

describe('GridComponent', () => {
  let component: GridComponent<Entity, Entity>;
  let fixture: ComponentFixture<GridComponent<Entity, Entity>>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GridComponent],
      providers: [
        { provide: DialogService, useValue: {} },
        { provide: Router, useValue: {} }
      ]
    })
    .compileComponents();

    fixture = TestBed.createComponent<GridComponent<Entity, Entity>>(GridComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
