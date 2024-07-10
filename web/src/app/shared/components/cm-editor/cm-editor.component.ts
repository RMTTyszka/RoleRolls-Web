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
import {BaseCrudService} from '../../base-service/base-crud-service';

@Component({
  selector: 'rr-cm-editor',
  templateUrl: './cm-editor.component.html',
  styleUrls: ['./cm-editor.component.css']
})
export class CmEditorComponent<T extends Entity, TCreateInput extends Entity> implements OnInit, OnDestroy {

  private unsubscriber = new Subject<void>();
  private subscriptions: Subscription[] = [];
  entity: T;
  public isLoading = true;
  @Input() entityId: string;
  @Input() disableSave = false;
  @Input() hasDelete = true;
  @Input() hasSave = true;
  @Input() disableDelete = false;
  @Input() action: EditorAction;
  @Input() service: BaseCrudService<T, TCreateInput>;
  @Input() requiredFields: string[] = [];
  @Input() form: FormGroup;
  @Input() v2: boolean;
  @Output() saved = new EventEmitter<T>();
  @Output() deleted = new EventEmitter<T>();
  @Output() loaded = new EventEmitter<T>();
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
      console.log(this.entity);
      this.isLoading = false;
      this.loaded.emit(entity);
    });
    this.service.onEntityChange.pipe(
      take(1)
    ).subscribe((entity: T) => {
      this.entity = entity;
      console.log(this.entity);
    });

  }
  ngOnDestroy(): void {
    this.unsubscriber.next();
    this.unsubscriber.complete();
  }

  canSave() {
    return this.form.valid && this.form.dirty && !this.disableSave;
  }

  getEntity(): Observable<any> {
    if (this.action === EditorAction.create) {
      return this.service.getNew();
    } else {
      return this.service.get(this.entityId);
    }
  }


  save() {
    /*console.log(JSON.stringify(entity));*/
    let subscription: Observable<BaseCrudResponse<T>>;
    if (this.action === EditorAction.create) {
      const entity: TCreateInput = this.form.getRawValue();
      subscription = this.v2 ? this.service.createV2(entity) : this.service.create(entity);
    } else if (this.action === EditorAction.update) {
      const entity: T = this.form.getRawValue();
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
        this.service.delete(this.entity.id).subscribe(resp => {
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
