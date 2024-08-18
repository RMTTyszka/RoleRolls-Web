import { Component, OnDestroy, OnInit } from '@angular/core';
import { LazyLoadEvent } from 'primeng/api';
import { PagedOutput } from 'src/app/shared/dtos/PagedOutput';
import { CampaignScene } from 'src/app/shared/models/pocket/campaigns/campaign-scene-model';
import { SubscriptionManager } from 'src/app/shared/utils/subscription-manager';
import { PocketRoll } from '../models/pocket-roll.model';
import { PocketCampaignDetailsService } from '../pocket-campaign-bodyshell/pocket-campaign-details.service';
import { PocketCampaignsService } from '../pocket-campaigns.service';
import {Observable, interval} from 'rxjs';
import {tap} from 'rxjs/operators';

@Component({
  selector: 'rr-campaign-rolls',
  templateUrl: './campaign-rolls.component.html',
  styleUrls: ['./campaign-rolls.component.scss']
})
export class CampaignRollsComponent implements OnInit, OnDestroy {
  public rolls: PocketRoll[] = [];
  public columns = [
    {
      header: 'Success', field: 'success'
    },
    {
      header: 'Creature', field: 'actorName'
    },
    {
      header: 'Property', field: 'propertyName'
    },
    {
      header: 'Dices', field: 'rolledDices'
    },
    {
      header: 'Date', field: 'dateTime'
    },
    {
      header: 'Description', field: 'description'
    },

];
  public loaded = false;

  private scene: CampaignScene;
  private subscriptionManager = new SubscriptionManager();
  private rollCount = 0;
  private rollsPerPage = 2;

  constructor(
    private readonly campaignsService: PocketCampaignsService,
    private readonly detailsService: PocketCampaignDetailsService
  ) {
    this.subscriptionManager.add('sceneChanged', this.detailsService.sceneChanged.subscribe((scene: CampaignScene) => {
      this.scene = scene;
      if (this.scene)
 {
  this.loaded = true;
  this.getList();
   }/*       this.getList({
        rows: 5,
        first: 0
      } as LazyLoadEvent); */
/*    this.subscriptionManager.add('pollingRolls', interval(5000).subscribe(() => {
      this.getList();
      }))*/
    }));

   }
  ngOnDestroy(): void {
    this.subscriptionManager.clear();
  }

  ngOnInit(): void {

  }
  public getList() {
    this.campaignsService.getSceneRolls(this.scene.campaignId, this.scene.id, 0, 99999999)
      .subscribe((result: PagedOutput<PocketRoll>) => {
        this.rolls = result.content;
      })
  }

}
