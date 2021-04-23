import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {FormGroup} from '@angular/forms';
import {BaseWeapon} from 'src/app/shared/models/BaseWeapon.model';
import {BaseWeaponService} from '../../base-weapons/base-weapon.service';
import {map, tap} from 'rxjs/operators';
import {createForm} from 'src/app/shared/EditorExtension';

@Component({
  selector: 'rr-base-weapon-select',
  templateUrl: './base-weapon-select.component.html',
  styleUrls: ['./base-weapon-select.component.css']
})
export class BaseWeaponSelectComponent implements OnInit {

  @Input() form: FormGroup;
  @Output() entitySelected = new EventEmitter<BaseWeapon>();
  result: string[] = [];
  entities: BaseWeapon[] = [];
  value: string;
  constructor(
    private service: BaseWeaponService
  ) { }

  ngOnInit() {
  }

  search(event) {
    this.service.getAllFiltered(event).pipe(
      tap(resp => this.entities = resp),
      map(resp => resp.map(race => race.name))
    ).subscribe(response => this.result = response);
  }
  selected(entityName: string) {
    const selectedEntity = this.entities.find(r => r.name === entityName);
    const form = new FormGroup({});
    createForm(form , selectedEntity);
    this.form.setControl('baseWeapon', form);
    this.entitySelected.emit(selectedEntity);
  }

}
