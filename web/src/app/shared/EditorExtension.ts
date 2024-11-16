import {FormArray, FormControl, FormGroup, UntypedFormArray, UntypedFormControl, UntypedFormGroup, Validators} from '@angular/forms';
import {Entity} from './models/Entity.model';
import {PocketCreature} from './models/pocket/creatures/pocket-creature';

export function getInput<T>(form: FormGroup, input: T): T {
  return {} as T;
}


export function createForm(form: FormGroup, entity: Entity, requiredFields: string[] = [], disabledFields: string[] = []) {
  Object.entries(entity).forEach((entry) => {
    // console.log(entry);
    if (entry[1] instanceof Array) {
      const array = new FormArray([]);
      entry[1].forEach(property => {
        if (property instanceof Object) {
          const newGroup: FormGroup = new FormGroup({});
          createForm(newGroup, property);
          array.push(newGroup);
        } else {
          array.push(new FormControl(property, []));
        }
      });
      form.addControl(entry[0], array);
    } else if (entry[1] instanceof Object) {
      const newGroup: FormGroup = new FormGroup({});
      createForm(newGroup, entry[1]);
      form.addControl(entry[0], newGroup);
    } else {
      form.addControl(entry[0], new FormControl(entry[1], []));
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
export function getAsForm(entity: any, requiredFields: string[] = [], disabledFields: string[] = []): FormGroup {
  const form = new FormGroup({});
  createForm(form, entity, requiredFields, disabledFields);
  return form;
}
export function ultraPatchValue(form: UntypedFormGroup, entity: Entity) {
  Object.entries(entity).forEach((entry) => {
    // console.log(entry);
    if (entry[1] instanceof Array) {
      const array = form.get(entry[0]) as UntypedFormArray;
      entry[1].forEach((property, index) => {
        if (property instanceof Object) {
          const newGroup = array.at(index) as UntypedFormGroup;
          ultraPatchValue(newGroup, property);
        } else {
          array.at(index).setValue(property);
        }
      });
    } else if (entry[1] instanceof Object) {
      const newGroup = form.get(entry[0]) as UntypedFormGroup;
      if (newGroup) {
        ultraPatchValue(newGroup, entry[1]);
      }
    } else {
      const control = form.get(entry[0]) as UntypedFormControl;
      if (control) {
        control.setValue(entry[1]);
      }
    }
  });
}
