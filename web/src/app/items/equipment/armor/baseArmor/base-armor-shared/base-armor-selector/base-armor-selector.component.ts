import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {FormGroup} from '@angular/forms';
import {BaseArmor} from '../../../../../../shared/models/BaseArmor.model';
import {BaseArmorService} from '../../base-armor.service';
import {map, tap} from 'rxjs/operators';
import {createForm} from '../../../../../../shared/EditorExtension';

@Component({
  selector: 'rr-base-armor-selector',
  templateUrl: './base-armor-selector.component.html',
  styleUrls: ['./base-armor-selector.component.css']
})
export class BaseArmorSelectorComponent implements OnInit {

  @Input() form: FormGroup;
  @Output() entitySelected = new EventEmitter<BaseArmor>();
  result: string[] = [];
  entities: BaseArmor[] = [];
  value: string;
  constructor(
    private service: BaseArmorService
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
    this.form.setControl('baseArmor', form);
    this.entitySelected.emit(selectedEntity);
  }

}
