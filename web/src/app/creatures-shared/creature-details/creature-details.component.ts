import {Component, Input, OnInit} from '@angular/core';
import {Creature} from '../../shared/models/creatures/Creature.model';
import {CreatureRollsService} from '../creature-rolls.service';
import {DialogService} from 'primeng/dynamicdialog';
import {RollDifficulty, RollsCardComponent} from '../rolls-card/rolls-card.component';
import {MessageService} from 'primeng/api';
import {SelectItem} from 'primeng/api/selectitem';
import {CampaignSessionService} from '../../campaign-session/campaign-session.service';

@Component({
  selector: 'loh-creature-details',
  templateUrl: './creature-details.component.html',
  styleUrls: ['./creature-details.component.css'],
  providers: [DialogService]
})
export class CreatureDetailsComponent implements OnInit {

  @Input() public creature: Creature;
  @Input() public isMaster = false;
  activeTab: 'attributes' | 'equipment' | 'inventory';
  tabs:  SelectItem[] = [
    {    label: 'Attributes', value: 'attributes'  },
    {    label: 'Equipment', value: 'equipment'  },
    {    label: 'Inventory', value: 'inventory'  },
    ];
  constructor(
    private campaignSessionService: CampaignSessionService
  ) { }

  ngOnInit(): void {
    this.activeTab = 'attributes';
  }

}
