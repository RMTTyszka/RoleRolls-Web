import {Component, EventEmitter, Input, OnDestroy, OnInit, Output} from '@angular/core';
import {BaseEntityService} from '../../base-entity-service';
import {Entity} from '../../models/Entity.model';
import {FormArray, FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {catchError, take, takeUntil} from 'rxjs/operators';
import {Observable, of, Subject, Subscription} from 'rxjs';
import {EditorAction} from '../../dtos/ModalEntityData';
import {createForm} from '../../EditorExtension';
import {ConfirmationService, MessageService} from 'primeng/api';
import {BaseCrudResponse} from '../../models/BaseCrudResponse';

@Component({
  selector: 'loh-cm-editor',
  templateUrl: './cm-editor.component.html',
  styleUrls: ['./cm-editor.component.css']
})
export class CmEditorComponent<T extends Entity> implements OnInit, OnDestroy {

  private unsubscriber = new Subject<void>();
  private subscriptions: Subscription[] = [];
  entity: T;
  public isLoading = true;
  @Input() entityId: string;
  @Input() disableSave = false;
  @Input() hasDelete = true;
  @Input() disableDelete = false;
  @Input() action: EditorAction;
  @Input() service: BaseEntityService<T>;
  @Input() requiredFields: string[] = [];
  @Input() form: FormGroup;
  @Output() saved = new EventEmitter<T>();
  @Output() deleted = new EventEmitter<T>();
  @Output() loaded = new EventEmitter<boolean>();
  constructor(
    private fb: FormBuilder,
    private messageService: MessageService,
    private confirmationService: ConfirmationService
  ) { }

  ngOnInit() {
    this.service.onSaveAction.pipe(
      takeUntil(this.unsubscriber)
    ).subscribe(() => {
      this.save();
    });
    console.log('about to get entity');
    this.getEntity().pipe(
      take(1)
    ).subscribe(entity => {
      this.entity = entity;
      if (!this.form) {
        this.form = this.fb.group({});
      }
      createForm(this.form, entity, this.requiredFields);
      console.log(this.form);
      this.isLoading = false;
      this.loaded.emit(true);
    });
    this.service.onEntityChange.pipe(
      take(1)
    ).subscribe((entity: T) => {
      this.entity = entity;
    });

  }
  ngOnDestroy(): void {
    this.unsubscriber.next();
    this.unsubscriber.complete();
  }

  canSave() {
    return this.form.valid && this.form.dirty && !this.disableSave;
  }

  getEntity() {
    if (this.action === EditorAction.create) {
      return this.service.getNewEntity();
    } else {
      return this.service.getEntity(this.entityId);
    }
  }

  createForm(form: FormGroup, entity: Entity) {

    Object.entries(entity).forEach((entry) => {
      console.log(entry);
      if (entry[1] instanceof Array) {
        const array = new FormArray([]);
        entry[1].forEach(property => {
          if (property instanceof Object) {
            const newGroup: FormGroup = new FormGroup({});
            this.createForm(newGroup, property);
            array.push(newGroup);
          } else {
            array.push(this.fb.control(property, []));
          }
        });
        form.addControl(entry[0], array);
      } else if (entry[1] instanceof Object) {
        const newGroup: FormGroup = new FormGroup({});
        this.createForm(newGroup, entry[1]);
        form.addControl(entry[0], newGroup);
      } else {

        form.addControl(entry[0], new FormControl(entry[1], []));
      }
    });
    this.requiredFields.forEach(field => {
      if (this.form.contains(field)) {
        this.form.get(field).setValidators(Validators.required);
      }
    });

  }

  save() {
    const entity: T = this.form.getRawValue();
    /*console.log(JSON.stringify(entity));*/
    console.log(entity);
    let subscription: Observable<BaseCrudResponse<T>>;
    if (this.action === EditorAction.create) {
      subscription = this.service.create(entity);
    } else if (this.action === EditorAction.update) {
      subscription = this.service.update(entity);
    }
    subscription.pipe(
      catchError((err, caught ) => {
        this.messageService.add({severity: 'error', detail: err.error.message});
        return of<BaseCrudResponse<T>>();
      })
    ).subscribe(response => {
      let messageType = '';
      if (response.success) {
        this.saved.next(response.entity);
        messageType = 'success';
        const newForm = new FormGroup({});
        createForm(newForm, response.entity);
        this.form.patchValue(newForm);

      } else {
        messageType = 'error';
      }
      this.messageService.add({severity: messageType, summary: '', detail: response.message});
    });

  }
  delete() {
    this.confirmationService.confirm({
      message: 'Are you sure?',
      accept: (() => {
        this.service.delete(this.entity).subscribe(resp => {
          let messageType = '';
          if (resp.success) {
            messageType = 'success';
            this.deleted.next(resp.entity);
          } else {
            messageType = 'error';
          }
          this.messageService.add({severity: messageType, summary: '', detail: resp.message});
        });
      })
    });

  }

}
