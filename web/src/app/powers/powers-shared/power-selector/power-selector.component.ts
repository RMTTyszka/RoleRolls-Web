import {Component, Injector, OnInit} from '@angular/core';
import {LegacyBaseSelectorComponent} from 'src/app/shared/legacy-base-selector/legacy-base-selector.component';
import {Power} from 'src/app/shared/models/Power.model';
import {PowersService} from '../../powers.service';
import {MatDialogRef} from '@angular/material/dialog';

@Component({
  selector: 'rr-power-selector',
  templateUrl: './power-selector.component.html',
  styleUrls: ['./power-selector.component.css']
})
export class PowerSelectorComponent extends LegacyBaseSelectorComponent<Power> implements OnInit {

  constructor(
    injector: Injector,
    protected dialogRef: MatDialogRef<PowerSelectorComponent>,
    _powersService: PowersService
  ) {
    super(injector, _powersService);
   }

  ngOnInit() {
    this.getAll();
  }

}
