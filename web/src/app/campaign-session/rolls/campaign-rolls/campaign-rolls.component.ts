import {Component, Input, OnInit} from '@angular/core';
import {CreatureRollsService} from '../../../creatures-shared/creature-rolls.service';
import {CreatureRollResult} from '../../../shared/models/rolls/CreatureRollResult';
import {CampaignSessionService} from '../../campaign-session.service';
import {switchMap} from 'rxjs/operators';

@Component({
  selector: 'loh-campaign-rolls',
  templateUrl: './campaign-rolls.component.html',
  styleUrls: ['./campaign-rolls.component.scss']
})
export class CampaignRollsComponent implements OnInit {
  rolls: CreatureRollResult[] = [];
  @Input() campaignId;
  constructor(
    private creatureRollsService: CreatureRollsService,
    private campaignSessionService: CampaignSessionService
  ) { }

  ngOnInit(): void {
    this.creatureRollsService.creatureRolled()
      .pipe(switchMap(result => {
        this.rolls.push(result);
        return this.campaignSessionService.saveRoll(this.campaignId, result);
      }))
      .subscribe();
  }

}
