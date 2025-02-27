import {
  FormArray,
  FormControl,
  FormGroup,
  isFormControl,
  UntypedFormArray,
  UntypedFormControl,
  UntypedFormGroup,
  Validators
} from '@angular/forms';
import { Entity } from '../models/Entity.model';
import { Property } from '@app/models/bonuses/bonus';

export function getInput<T>(form: FormGroup, input: T): T {
  return {} as T;
}
function isProperty(obj: any): obj is Property {
  return obj && typeof obj === 'object' && 'propertyId' in obj && 'type' in obj;
}

export function createForm(form: FormGroup, entity: Entity, empty: boolean = false, requiredFields: string[] = [], disabledFields: string[] = []) {
  Object.entries(entity).forEach(([key, value]) => {
    if (value instanceof Array) {
      const array = new FormArray<any>([]);
      value.forEach(property => {
        if (property instanceof Object) {
          if (isProperty(property)) {
            array.push(new FormControl(property, []));
          } else {
            const newGroup: FormGroup = new FormGroup({});
            createForm(newGroup, property, empty);
            array.push(newGroup);
          }
        } else {
          array.push(new FormControl(property, []));
        }
      });
      form.addControl(key, array);
    } else if (value instanceof Object) {
      if (isProperty(value)) {
        form.addControl(key, new FormControl(value, []));
      } else {
        const newGroup: FormGroup = new FormGroup({});
        createForm(newGroup, value, empty);
        form.addControl(key, newGroup);
      }
    } else {
      form.addControl(key, new FormControl(empty ? null : value, []));
    }
  });

  requiredFields.forEach(field => {
    const control = form.get(field);
    if (control) {
      control.setValidators(Validators.required);
    }
  });

  disabledFields.forEach(field => {
    const control = form.get(field);
    if (control) {
      control.disable();
    }
  });
}

export function getAsForm(entity: any, empty: boolean = false, requiredFields: string[] = [], disabledFields: string[] = []): FormGroup {
  const form = new FormGroup({});
  createForm(form, entity, empty, requiredFields, disabledFields);
  return form;
}
export function ultraPatchValue(form: UntypedFormGroup, entity: Entity, entityName: string = '') {
  Object.entries(entity).forEach((entry) => {
    if (entry[1] instanceof Array) {
      const array = form.get(entry[0]) as UntypedFormArray;
      array.clear({emitEvent: false});
      entry[1].forEach((property, index) => {
        if (property instanceof Object) {
          array.push(getAsForm(property, false));
        } else {
          array.push(property);
        }
      });
    } else if (entry[1] instanceof Object) {
      let newGroup = form.get(entry[0]) as UntypedFormGroup;
      if (newGroup instanceof FormControl) {
        newGroup = getAsForm(entry[1], false);
        form.removeControl(entry[0]);
        form.addControl(entry[0] , newGroup);
        ultraPatchValue(newGroup, entry[1], entry[0]);
      } else {
        ultraPatchValue(newGroup, entry[1], entry[0]);
      }
    } else {
      const control = form.get(entry[0]) as UntypedFormControl;
      if (control) {
        if (control instanceof FormGroup && entry[1] == null) {
          (control.parent as UntypedFormGroup).removeControl(entry[0]);
          (control.parent as UntypedFormGroup).addControl(entry[0], new FormControl(null));
        } else {
          control.setValue(entry[1]);
        }
      }
    }
  });
}
