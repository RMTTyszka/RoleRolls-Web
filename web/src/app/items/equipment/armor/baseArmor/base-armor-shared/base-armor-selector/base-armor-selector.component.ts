import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {ArmorCategory} from '../../../../../../shared/models/ArmorCategory.model';
import {FormControl, FormGroup} from '@angular/forms';
import {ArmorCategoryService} from '../../../armor-category.service';
import {BaseArmor} from '../../../../../../shared/models/BaseArmor.model';
import {BaseArmorService} from '../../base-armor.service';
import {map, tap} from 'rxjs/operators';
import {createForm} from '../../../../../../shared/EditorExtension';
import {Race} from '../../../../../../shared/models/Race.model';
import {RaceService} from '../../../../../../races/race-editor/race.service';

@Component({
  selector: 'loh-base-armor-selector',
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
