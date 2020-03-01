import {Component, Injector, OnInit} from '@angular/core';
import {RacesService} from '../races.service';
import {BaseListComponent} from '../../shared/base-list/base-list.component';
import {RaceEditorComponent} from '../race-editor/race-editor.component';
import {Race} from '../../shared/models/Race.model';

@Component({
  selector: 'loh-races',
  templateUrl: './races.component.html',
  styleUrls: ['./races.component.css']
})
export class RacesComponent extends BaseListComponent<Race> implements OnInit {

  columnsToDisplay = ['id', 'name', 'power', 'traits', 'actions'];

  constructor(
    injector: Injector,
    protected service: RacesService,
  ) {
    super(injector, service);
    this.editor = RaceEditorComponent;
  }

  ngOnInit() {
    this.getAllFiltered();
  }

  create() {
    this.edit(new Race());
  }





}
