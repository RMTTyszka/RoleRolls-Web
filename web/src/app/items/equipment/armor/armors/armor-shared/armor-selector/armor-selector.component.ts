import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {FormGroup, FormGroupDirective} from '@angular/forms';
import {map, tap} from 'rxjs/operators';
import {createForm} from '../../../../../../shared/EditorExtension';
import {ArmorService} from '../../armor.service';
import { ArmorModel } from 'src/app/shared/models/ArmorModel.model';

@Component({
  selector: 'loh-armor-selector',
  templateUrl: './armor-selector.component.html',
  styleUrls: ['./armor-selector.component.css']
})
export class ArmorSelectorComponent implements OnInit {

  @Output() entitySelected = new EventEmitter<ArmorModel>();
  form: FormGroup;
  @Input() groupName: string;
  result: string[] = [];
  entities: ArmorModel[] = [];
  value: string;
  constructor(
    private service: ArmorService,
    private formGroupDirective: FormGroupDirective  ) { }

  ngOnInit() {
    this.form = this.formGroupDirective.form.get(this.groupName) as FormGroup;
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
    this.form.setControl('armor', form);
    this.entitySelected.emit(selectedEntity);
  }

}
