import { TestBed } from '@angular/core/testing';

import { CreatureEquipmentService } from './creature-equipment.service';

describe('CreatureEquipmentService', () => {
  let service: CreatureEquipmentService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CreatureEquipmentService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
