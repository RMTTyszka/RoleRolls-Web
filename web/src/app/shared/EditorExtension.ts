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
export function ultraPatchValue(form: UntypedFormGroup, entity: Entity, entityName: string) {
  Object.entries(entity).forEach((entry) => {
    if (entry[1] instanceof Array) {
      const array = form.get(entry[0]) as UntypedFormArray;
      array.clear({emitEvent: false});
      entry[1].forEach((property, index) => {
        if (property instanceof Object) {
          array.push(getAsForm(property));
        } else {
          array.push(property);
        }
      });
    } else if (entry[1] instanceof Object) {
      let newGroup = form.get(entry[0]) as UntypedFormGroup;
      if (newGroup instanceof FormControl) {
        newGroup = getAsForm(entry[1]);
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
