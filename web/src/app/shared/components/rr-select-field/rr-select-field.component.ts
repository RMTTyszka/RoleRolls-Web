import {Component, EventEmitter, forwardRef, Input, OnInit, Output} from '@angular/core';
import {BaseCrudService} from '../../base-service/base-crud-service';
import {DialogService} from 'primeng/dynamicdialog';
import {ControlValueAccessor, FormGroup, FormGroupDirective, NG_VALUE_ACCESSOR} from '@angular/forms';
import {Entity} from '../../models/Entity.model';
import {RrSelectModalComponent} from '../rr-select-modal/rr-select-modal.component';
import {createForm} from '../../EditorExtension';
import {FormManipulator} from '../../utils/form-manipulator';

export interface RRSelectModalInjector<T extends Entity> {
  service: BaseCrudService<T, T>;
}

@Component({
  selector: 'loh-rr-select-field',
  templateUrl: './rr-select-field.component.html',
  styleUrls: ['./rr-select-field.component.css'],
  providers: [DialogService],
})
export class RrSelectFieldComponent<T extends Entity> implements OnInit {

  @Input() service: BaseCrudService<T, T>;
  @Input() controlName: string;
  @Input() initialValue: any;

  @Output() entitySelected = new EventEmitter<T>();
  placeHolder: string;
  modalTitle: string;
  fieldName: string;

  form: FormGroup;
  descriptionValue: string;
  public loaded = false;

  constructor(
    private dialog: DialogService,
    private formGroupDirective: FormGroupDirective
  ) {
  }

  ngOnInit(): void {
    this.form = this.formGroupDirective.form || null;
    this.placeHolder = this.service.selectPlaceholder;
    this.fieldName = this.service.fieldName;
    this.modalTitle = this.service.selectModalTitle;
    if (this.initialValue) {
      this.descriptionValue = this.controlName ? this.initialValue : this.initialValue[this.fieldName];
    }
    this.loaded = true;
  }

  open() {
    this.dialog.open(RrSelectModalComponent, {
      data: <RRSelectModalInjector<T>> {
        service: this.service
      }
    }).onClose.subscribe(entity => {
      if (entity) {
        this.entitySelected.next(entity);
        this.descriptionValue = entity[this.fieldName];
        if (this.controlName) {
          const form = new FormGroup({});
          createForm(form, entity)
          this.form.get(this.controlName).patchValue(entity);
        }
        this.entitySelected.next(entity);
      }
    });
  }

}
