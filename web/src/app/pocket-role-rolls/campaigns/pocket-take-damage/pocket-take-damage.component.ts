import { Subject } from 'rxjs';
import { TakeDamageInput } from './../../../shared/models/inputs/TakeDamageInput';
import { PocketCampaignModel } from './../../../shared/models/pocket/campaigns/pocket.campaign.model';
import { Component, OnInit, Input } from '@angular/core';
import { CampaignScene } from 'src/app/shared/models/pocket/campaigns/campaign-scene-model';

@Component({
  selector: 'rr-pocket-take-damage',
  templateUrl: './pocket-take-damage.component.html',
  styleUrls: ['./pocket-take-damage.component.scss']
})
export class PocketTakeDamageComponent implements OnInit {
  @Input() public inputEmitter: Subject<TakeDamageInput>;
  @Input() public campaign: PocketCampaignModel;
  @Input() public scene: CampaignScene;
  constructor() { }

  ngOnInit(): void {
  }

}
