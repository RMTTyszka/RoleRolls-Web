import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {FormGroup} from '@angular/forms';
import {Inventory} from '../../../shared/models/Inventory.model';
import {RingInstance} from '../../../shared/models/RingInstance.model';
import {isRing} from '../../../shared/utils/isItem';

export interface SelectedRing {
  isLeft: boolean;
  isRight: boolean;
  ring: RingInstance;
}
export enum RingHand {
  right, left
}

@Component({
  selector: 'rr-inventory-ring-select',
  templateUrl: './inventory-ring-select.component.html',
  styleUrls: ['./inventory-ring-select.component.css']
})
export class InventoryRingSelectComponent implements OnInit {
  @Input() ringHand: RingHand;
  @Output() ringSelected = new EventEmitter<SelectedRing>();
  constructor() {}

  ngOnInit() {
  }
  filter = (inventoryForm: FormGroup) => (inventoryForm.value as Inventory).items
    .filter(item => isRing(item)) as RingInstance[]

  search = (filter: string, items: Array<RingInstance>) => items
    .filter((item: RingInstance) => item.name.includes(filter)
      || item.ringModel.name.includes(filter))
    .map(item => item.name)

  itemSelected(selectedRing: RingInstance) {
    this.ringSelected.next(<SelectedRing> {
      isLeft: this.ringHand === RingHand.left,
      isRight: this.ringHand === RingHand.right,
      ring: selectedRing
    });
  }
  get placeholder() {
    return this.ringHand === RingHand.right ? 'Right Ring' : 'Left Ring';
  }
  get formName() {
    return this.ringHand === RingHand.right ? 'ringRight' : 'ringLeft';
  }
}
