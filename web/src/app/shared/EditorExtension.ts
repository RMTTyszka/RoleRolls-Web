import {FormArray, FormControl, FormGroup, Validators} from '@angular/forms';
import {Entity} from './models/Entity.model';

export function createForm(form: FormGroup, entity: Entity, requiredFields: string[] = []) {
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
}
