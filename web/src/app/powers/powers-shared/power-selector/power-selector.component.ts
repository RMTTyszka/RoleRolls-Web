import { Component, OnInit, Injector } from '@angular/core';
import { BaseSelectorComponent } from 'src/app/shared/base-selector/base-selector/base-selector.component';
import { Power } from 'src/app/shared/models/Power.model';
import { PowersService } from '../../powers.service';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'loh-power-selector',
  templateUrl: './power-selector.component.html',
  styleUrls: ['./power-selector.component.css']
})
export class PowerSelectorComponent extends BaseSelectorComponent<Power> implements OnInit {

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
