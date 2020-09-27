import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {BaseCrudService} from '../../base-service/base-crud-service';
import {DialogService} from 'primeng/dynamicdialog';
import {FormGroup, FormGroupDirective} from '@angular/forms';
import {Entity} from '../../models/Entity.model';
import {RrSelectModalComponent} from '../rr-select-modal/rr-select-modal.component';
import {createForm} from '../../EditorExtension';

export interface RRSelectModalInjector<T extends Entity> {
  service: BaseCrudService<T>;
}

@Component({
  selector: 'loh-rr-select-field',
  templateUrl: './rr-select-field.component.html',
  styleUrls: ['./rr-select-field.component.css'],
  providers: [DialogService]
})
export class RrSelectFieldComponent<T extends Entity> implements OnInit {

  @Input() service: BaseCrudService<T>;
  @Input() formControlName: string;
  @Input() initialValue: any;

  @Output() entitySelected = new EventEmitter<T>();
  placeHolder: string;
  modalTitle: string;
  fieldName: string;

  form: FormGroup;
  value: string;

  constructor(
    private dialog: DialogService,
    private formGroupDirective: FormGroupDirective
  ) { }

  ngOnInit(): void {
    this.form = this.formGroupDirective.form || null;
    this.placeHolder = this.service.selectPlaceholder;
    this.fieldName = this.service.fieldName;
    this.modalTitle = this.service.selectModalTitle;
    if (this.initialValue) {
      this.value = this.formControlName ? this.initialValue : this.initialValue[this.fieldName];
    }
  }

  open() {
    this.dialog.open(RrSelectModalComponent, {
      data: <RRSelectModalInjector<T>> {
        service: this.service
      }
    }).onClose.subscribe(entity => {
      if (entity) {
        this.entitySelected.next(entity);
        if (!this.formControlName) {
          this.value = entity[this.fieldName];
        } else {
          this.form.get(this.formControlName).setValue(createForm(new FormGroup({}), entity));
        }
        this.entitySelected.next(entity);
      }
    });
  }

}
