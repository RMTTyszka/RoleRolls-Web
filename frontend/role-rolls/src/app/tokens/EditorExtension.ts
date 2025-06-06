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
export interface FormObject {
  [key: string]: any;
}
export interface FormConfig {
  empty?: boolean;
  requiredFields?: string[];
  disabledFields?: string[];
  recursive?: boolean;
}
export interface PatchOptions {
  entityName?: string;
}
export function createForm(
  form: FormGroup,
  entity: FormObject,
  config: FormConfig = {}
): void {
  const {
    empty = false,
    requiredFields = [],
    disabledFields = [],
    recursive = true
  } = config;

  Object.entries(entity).forEach(([key, value]) => {
    if (value instanceof Array && recursive) {
      const array = new FormArray<any>([]);
      value.forEach(property => {
        if (property instanceof Object && recursive) {
          if (isProperty(property)) {
            array.push(new FormControl(property, []));
          } else {
            const newGroup: FormGroup = new FormGroup({});
            createForm(newGroup, property, { empty, requiredFields, disabledFields, recursive });
            array.push(newGroup);
          }
        } else {
          array.push(new FormControl(property, []));
        }
      });
      form.addControl(key, array);
    } else if (value instanceof Object && recursive) {
      if (isProperty(value)) {
        form.addControl(key, new FormControl(value, []));
      } else {
        const newGroup: FormGroup = new FormGroup({});
        createForm(newGroup, value, { empty, requiredFields, disabledFields, recursive });
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

export function getAsForm(
  entity: FormObject,
  config: FormConfig = {}
): FormGroup {
  const form = new FormGroup({});
  createForm(form, entity, config);
  return form;
}

export function ultraPatchValue(
  form: UntypedFormGroup,
  entity: FormObject,
  options: PatchOptions = {}
): void {
  const { entityName = '' } = options;

  Object.entries(entity).forEach((entry) => {
    if (entry[1] instanceof Array) {
      const array = form.get(entry[0]) as UntypedFormArray;
      array.clear({ emitEvent: false });
      entry[1].forEach((property, index) => {
        if (property instanceof Object) {
          array.push(getAsForm(property, { empty: false }));
        } else {
          array.push(property);
        }
      });
    } else if (entry[1] instanceof Object) {
      let newGroup = form.get(entry[0]) as UntypedFormGroup;
      if (newGroup instanceof FormControl) {
        newGroup = getAsForm(entry[1], { empty: false });
        form.removeControl(entry[0]);
        form.addControl(entry[0], newGroup);
        ultraPatchValue(newGroup, entry[1], { entityName: entry[0] });
      } else {
        ultraPatchValue(newGroup, entry[1], { entityName: entry[0] });
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
