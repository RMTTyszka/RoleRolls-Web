import { Component, OnDestroy, OnInit } from '@angular/core';
import { LazyLoadEvent } from 'primeng/api';
import { PagedOutput } from 'src/app/shared/dtos/PagedOutput';
import { CampaignScene } from 'src/app/shared/models/pocket/campaigns/campaign-scene-model';
import { SubscriptionManager } from 'src/app/shared/utils/subscription-manager';
import { PocketRoll } from '../models/pocket-roll.model';
import { PocketCampaignDetailsService } from '../pocket-campaign-bodyshell/pocket-campaign-details.service';
import { PocketCampaignsService } from '../pocket-campaigns.service';

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
/*       this.getList({
        rows: 5,
        first: 0
      } as LazyLoadEvent); */
      this.loaded = true;
    }));

   }
  ngOnDestroy(): void {
    this.subscriptionManager.clear();
  }

  ngOnInit(): void {
  }
  public getList(event: LazyLoadEvent) {
    this.campaignsService.getSceneRolls(this.scene.campaignId, this.scene.id, event.first, event.rows)
    .subscribe((result: PagedOutput<PocketRoll>) => {

      if (this.rolls.length !== result.totalElements) {
        this.rolls = Array.from<PocketRoll>({length: result.totalElements});
      }

      //populate page of virtual cars
      Array.prototype.splice.apply(this.rolls, [...[event.first, event.rows], ...result.content]);

      //trigger change detection
      this.rolls = [...this.rolls];

    });
  }

}
