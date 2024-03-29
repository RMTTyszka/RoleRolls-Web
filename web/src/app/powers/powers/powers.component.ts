import {Component, Injector, OnInit} from '@angular/core';
import {PowersService} from '../powers.service';
import {LegacyBaseListComponent} from 'src/app/shared/base-list/legacy-base-list.component';
import {Power} from 'src/app/shared/models/Power.model';
import {PowerEditorComponent} from './power-editor/power-editor.component';

@Component({
  selector: 'rr-powers',
  templateUrl: './powers.component.html',
  styleUrls: ['./powers.component.css']
})
export class PowersComponent extends LegacyBaseListComponent<Power> implements OnInit {

  powers: string[];
  constructor(
    injector: Injector,
    protected service: PowersService
  ) {
    super(injector, service);
    this.editor = PowerEditorComponent;
   }

  ngOnInit() {
    this.getAll();
  }

}
