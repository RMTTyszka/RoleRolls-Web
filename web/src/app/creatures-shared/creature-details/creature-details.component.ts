import {Component, Input, OnInit} from '@angular/core';
import {Creature} from '../../shared/models/creatures/Creature.model';
import {CreatureRollsService} from '../creature-rolls.service';
import {CampaignRollsService} from '../../campaign-session/rolls/campaign-rolls.service';

@Component({
  selector: 'loh-creature-details',
  templateUrl: './creature-details.component.html',
  styleUrls: ['./creature-details.component.css']
})
export class CreatureDetailsComponent implements OnInit {

  @Input() creature: Creature;
  constructor(
    private readonly creatureRollsService: CreatureRollsService,
  ) { }

  ngOnInit(): void {
  }


  roll(property: string) {
    this.creatureRollsService.roll(this.creature.id, property, null, null).subscribe((result) => {
      this.creatureRollsService.emitCreatureRolled(result);
    });
  }

}
