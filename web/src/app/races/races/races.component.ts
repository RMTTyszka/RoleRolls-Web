import {Component, Injector, OnInit} from '@angular/core';
import {RacesService} from '../races.service';
import {LegacyBaseListComponent} from '../../shared/base-list/legacy-base-list.component';
import {RaceEditorComponent} from '../race-editor/race-editor.component';
import {Race} from '../../shared/models/Race.model';

@Component({
  selector: 'loh-races',
  templateUrl: './races.component.html',
  styleUrls: ['./races.component.css']
})
export class RacesComponent  implements OnInit {

  constructor(
    public service: RacesService
  ) {

  }

  ngOnInit() {
  }






}
