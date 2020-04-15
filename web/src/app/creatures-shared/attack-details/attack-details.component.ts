import {Component, Input, OnInit} from '@angular/core';
import {AttackDetails} from '../../shared/models/AttackDetails.model';

@Component({
  selector: 'loh-attack-details',
  templateUrl: './attack-details.component.html',
  styleUrls: ['./attack-details.component.css']
})
export class AttackDetailsComponent implements OnInit {
  @Input() attackDetails: AttackDetails;
  constructor() { }

  ngOnInit() {
  }

}
