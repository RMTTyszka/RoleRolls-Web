import {Injector, OnDestroy, OnInit} from '@angular/core';
import {Entity} from '../models/Entity.model';
import {FormArray, FormBuilder, FormControl, FormGroup} from '@angular/forms';
import {Router} from '@angular/router';
import {DialogService, DynamicDialogRef} from 'primeng/dynamicdialog';
import {BaseCrudService} from '../base-service/base-crud-service';
import {EditorAction} from '../dtos/ModalEntityData';

export interface IEditorInput<T> {
  entity: T;
}

export class BaseCreatorComponent<T extends Entity, TCreateInput> {
  entityIsLoading = true;
  isLoading = true;
  hasLoaded = false;
  action: EditorAction = EditorAction.update;
  entity: T;
  createEntity: TCreateInput;
  form: FormGroup = new FormGroup({});
  fb: FormBuilder;
  router: Router;

  protected service: BaseCrudService<T, TCreateInput>;
  protected dialog: DialogService;
  protected dialogRef: DynamicDialogRef;
  constructor(
    protected injector: Injector,
    ) {
      this.dialog = injector.get(DialogService);
      this.dialogRef = injector.get(DynamicDialogRef);
      this.fb = injector.get(FormBuilder);
      this.router = injector.get(Router);
     }

  getEntity(id?: string) {
    if (id) {
      this.action = EditorAction.update;
      this.service.get(id).subscribe(entity => {

        this.entity = entity;
        console.log(JSON.stringify(this.entity));

        this.createForm(this.form, this.entity);
        this.isLoading = false;
        this.hasLoaded = true;
        this.entityIsLoading = false;
        console.log(this.form.value);
        this.afterGetEntity();
      });
    } else {
      this.service.getNew().subscribe(newEntity => {
        this.action = EditorAction.create;
        this.createEntity = newEntity;
        console.log(JSON.stringify(newEntity));
        this.createForm(this.form, newEntity);
        this.isLoading = false;
        this.hasLoaded = true;
        this.entityIsLoading = false;
        console.log(this.form.value);
        this.afterGetEntity();
      });
    }
  }

  protected afterGetEntity() {

    return;
  }

  createForm(form: FormGroup, entity: any) {
    Object.entries(entity).forEach((entry) => {
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

  }

  save() {
    if (this.action === EditorAction.create) {
      const entity: TCreateInput =  Object.assign(this.form.getRawValue());
      console.log(JSON.stringify(entity));
      this.service.create(entity).subscribe(updatedEntity => {
          this.dialogRef.close();
        }
      );
    } else if (this.action === EditorAction.update) {
      const entity: T =  Object.assign(this.form.getRawValue());
      console.log(JSON.stringify(entity));
      this.service.update(entity).subscribe(updatedEntity => {
          this.dialogRef.close();
        }
      );
    }
  }
  delete() {
    this.service.delete(this.entity.id).subscribe(resp => {
      this.dialogRef.close();
    });
  }
  cancel() {
    this.dialogRef.close();
  }
}
