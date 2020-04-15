import {Component, Input, OnInit, Output} from '@angular/core';
import {PowerManagementService} from './power-management.service';
import {Power} from 'src/app/shared/models/Power.model';


@Component({
  selector: 'loh-power-management',
  templateUrl: './power-management.component.html',
  styleUrls: ['./power-management.component.css']
})
export class PowerManagementComponent implements OnInit {

  @Input() powers: Power[];

  @Output() updatedPowers: Power[];
  constructor(
    private _service: PowerManagementService
  ) { }

  ngOnInit() {

  }



}
