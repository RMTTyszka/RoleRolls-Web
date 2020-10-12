import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {FormGroup} from '@angular/forms';
import {RaceService} from '../../race-editor/race.service';
import {Race} from '../../../shared/models/Race.model';
import {map, tap} from 'rxjs/operators';
import {createForm} from '../../../shared/EditorExtension';

@Component({
  selector: 'loh-race-select',
  templateUrl: './race-select.component.html',
  styleUrls: ['./race-select.component.css']
})
export class RaceSelectComponent implements OnInit {

  @Input() form: FormGroup;
  @Output() raceSelected = new EventEmitter<Race>();
  result: string[] = [];
  races: Race[] = [];
  value: string;
  constructor(
    private service: RaceService
  ) { }

  ngOnInit() {
  }

  search(event) {
    this.service.list(event).pipe(
      tap(resp => this.races = resp.content),
      map(resp => resp.content.map(race => race.name))
    ).subscribe(response => this.result = response);
  }
  selected(race: string) {
    const selectedRace = this.races.find(r => r.name === race);
    const form = new FormGroup({});
    createForm(form , selectedRace);
    this.form.setControl('race', form);
    this.raceSelected.emit(selectedRace);
  }

}
