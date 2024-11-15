import {Component, Input, OnDestroy, OnInit} from '@angular/core';
import {Creature} from '../../shared/models/creatures/Creature.model';
import {CreatureRollsService} from '../creature-rolls.service';
import {DialogService} from 'primeng/dynamicdialog';
import {RollDifficulty, RollsCardComponent} from '../rolls-card/rolls-card.component';
import {MessageService} from 'primeng/api';
import {SelectItem} from 'primeng/api/selectitem';
import {CampaignSessionService} from '../../campaign-session/campaign-session.service';
import {Campaign} from '../../shared/models/campaign/Campaign.model';
import {Subject} from 'rxjs';
import {takeUntil} from 'rxjs/operators';

@Component({
  selector: 'rr-creature-details',
  templateUrl: './creature-details.component.html',
  styleUrls: ['./creature-details.component.css'],
  providers: [DialogService]
})
export class CreatureDetailsComponent implements OnInit, OnDestroy {


  @Input() public creature: Creature;
  @Input() public isMaster = false;
  private campaign: Campaign;
  private unsubscriber = new Subject<void>();

  activeTab: 'attributes' | 'equipment' | 'inventory';
  tabs:  SelectItem[] = [
    {    label: 'Attributes', value: 'attributes'  },
    {    label: 'Equipment', value: 'equipment'  },
    {    label: 'Inventory', value: 'inventory'  },
    ];
  constructor(
    private campaignSessionService: CampaignSessionService
  ) {
    this.campaignSessionService.campaignChanged
      .pipe(takeUntil(this.unsubscriber)).subscribe(campaign => this.campaign = campaign);
  }

  ngOnInit(): void {
    this.activeTab = 'attributes';
  }
  ngOnDestroy(): void {
    this.unsubscriber.next();
  }

}
