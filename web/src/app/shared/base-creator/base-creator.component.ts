import {Injector, OnDestroy, OnInit} from '@angular/core';
import {Entity} from '../models/Entity.model';
import {FormArray, FormBuilder, FormControl, FormGroup} from '@angular/forms';
import {Subscription} from 'rxjs';
import {DataService} from '../data.service';
import {Router} from '@angular/router';
import {DialogService, DynamicDialogRef} from 'primeng/dynamicdialog';
import {BaseCrudService} from '../base-service/base-crud-service';

export interface IEditorInput<T> {
  entity: T;
}

export class BaseCreatorComponent<T extends Entity> implements OnInit, OnDestroy {
  entityIsLoading = true;
  isLoading = true;
  action = 'create';
  entity: T;
  form: FormGroup = new FormGroup({});
  fb: FormBuilder;
  router: Router;

  protected service: BaseCrudService<T>;
  myService: string;
  protected dataService: DataService;

  protected dialog: DialogService;
  protected dialogRef: DynamicDialogRef;
  protected entitySubscription: Subscription;
  constructor(
    protected injector: Injector,
    ) {
      this.dialog = injector.get(DialogService);
      this.dataService = injector.get(DataService);
      this.fb = injector.get(FormBuilder);
      this.router = injector.get(Router);

     }

  ngOnInit() {
    this.getEntity();
  }
  getEntity(id?: string) {

    if (id) {
      this.action = 'update';
      this.service.get(id).subscribe(entity => {

        this.entity = entity;
        console.log(JSON.stringify(this.entity));

        this.createForm(this.form, this.entity);
        this.isLoading = false;
        this.entityIsLoading = false;
        console.log(this.form.value);
        this.afterGetEntity();
      });
    } else {
      this.service.getNew().subscribe(newEntity => {
        this.entity = newEntity;
      });
      console.log(JSON.stringify(this.entity));
      this.createForm(this.form, this.entity);
      this.isLoading = false;
      this.entityIsLoading = false;
      console.log(this.form.value);

      this.afterGetEntity();
    }
  }

  ngOnDestroy(): void {
  }

  protected afterGetEntity() {

    return;
  }

  createForm(form: FormGroup, entity: Entity) {

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
    const entity: T =  Object.assign(this.form.getRawValue());
    console.log(JSON.stringify(entity));
    if (this.action === 'create') {
      this.service.create(entity).subscribe(updatedEntity => {
          this.dialogRef.close();
        }
      );
    } else if (this.action === 'update') {
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
