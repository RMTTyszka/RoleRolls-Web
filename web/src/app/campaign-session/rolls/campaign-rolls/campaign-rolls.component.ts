import {Component, Input, OnInit} from '@angular/core';
import {CreatureRollsService} from '../../../creatures-shared/creature-rolls.service';
import {CreatureRollResult} from '../../../shared/models/rolls/CreatureRollResult';
import {CampaignSessionService} from '../../campaign-session.service';
import {switchMap} from 'rxjs/operators';
import {Roll} from '../../../shared/models/rolls/Roll';

@Component({
  selector: 'loh-campaign-rolls',
  templateUrl: './campaign-rolls.component.html',
  styleUrls: ['./campaign-rolls.component.scss']
})
export class CampaignRollsComponent implements OnInit {
  rolls: CreatureRollResult[] = [];
  @Input() campaignId;
  public selectedRoll: Roll = new Roll();

  public get lastRoll() {
    return this.rolls[this.rolls.length - 1];
  }
  constructor(
    private creatureRollsService: CreatureRollsService,
    private campaignSessionService: CampaignSessionService
  ) { }

  ngOnInit(): void {
    this.campaignSessionService.getRolls(this.campaignId).subscribe((rolls) => {
      rolls = rolls.reverse();
      this.rolls.push(...rolls);
    })
    this.creatureRollsService.creatureRolled()
      .pipe(switchMap(result => {
        this.rolls.splice(0, 0, result);
        return this.campaignSessionService.saveRoll(this.campaignId, result);
      }))
      .subscribe();
  }

  selectRoll(roll: Roll) {
    this.selectedRoll = roll;
  }

}
