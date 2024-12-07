import { Component, OnDestroy, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { Subject } from 'rxjs';
import { AuthenticationService } from 'src/app/authentication/authentication.service';
import { CreatureType } from 'src/app/shared/models/creatures/CreatureType';
import { CampaignScene } from 'src/app/shared/models/pocket/campaigns/campaign-scene-model';
import { PocketCampaignModel } from 'src/app/shared/models/pocket/campaigns/pocket.campaign.model';
import { PocketCreature, PocketMonster } from 'src/app/shared/models/pocket/creatures/pocket-creature';
import { SubscriptionManager } from 'src/app/shared/utils/subscription-manager';
import { PocketCampaignDetailsService } from '../pocket-campaign-bodyshell/pocket-campaign-details.service';
import { DialogService } from 'primeng/dynamicdialog';
import { EditorAction } from 'src/app/shared/dtos/ModalEntityData';
import { SceneCreature } from 'src/app/shared/models/pocket/campaigns/scene-creature.model';
import {RollInput} from "../../models/RollInput";
import {PocketCampaignsService} from "../../pocket-campaigns.service";
import {PocketCreatureEditorComponent} from "../../../pocket-creature-editor/pocket-creature-editor.component";

@Component({
  selector: 'rr-campaign-monsters',
  templateUrl: './campaign-monsters.component.html',
  styleUrls: ['./campaign-monsters.component.scss']
})
export class CampaignMonstersComponent implements OnInit, OnDestroy {
  public monsters: PocketMonster[] = [];

  public get isMaster() {
    return this.campaign.masterId === this.authService.userId;
  }
  public get loaded() {
    return this.campaign && this.scene;
  }
  public rollOptions: MenuItem[] = [];
  public displayRollSidebar = false;
  public rollInputEmitter = new Subject<RollInput>();
  public scene: CampaignScene = new CampaignScene();
  public campaign: PocketCampaignModel = new PocketCampaignModel();
  public creatureTypeEnum = CreatureType;
  private subscriptionManager = new SubscriptionManager();

  constructor(
    private readonly campaignService: PocketCampaignsService,
    private readonly detailsService: PocketCampaignDetailsService,
    private readonly dialogService: DialogService,
    private authService: AuthenticationService,
  ) {
    this.subscribeToCampaignLoaded();
    this.subscribeToSceneChanges();
   }

   public createAndAddMonster() {
    this.dialogService.open(PocketCreatureEditorComponent, {
      width: '100vw',
      height: '100vh',
      data: {
        campaign: this.campaign,
        action: EditorAction.create,
        creatureType: CreatureType.Monster
      },
    });
   }

   public addExistingMonster() {

   }
   public removeMonster(monster: PocketMonster) {
    this.campaignService.removeCreatureFromScene(this.campaign.id, this.scene.id, monster.id).subscribe(() => {
      this.refreshMonsteres();
    });
   }
   public addMonster(creature: PocketCreature){
    const input = {
      creatureId: creature.id,
      hidden: false
    } as SceneCreature;
    this.campaignService.addMonsterToScene(this.campaign.id, this.scene.id, input)
    .subscribe(() => {
      this.detailsService.monsterAddedToScene.next();
      this.refreshMonsteres();
    });
  }
  private subscribeToCampaignLoaded() {
    this.subscriptionManager.add('campaignLoaded', this.detailsService.campaignLoaded.subscribe((campaign: PocketCampaignModel) => {
        this.campaign = campaign;
    }));
  }

  private subscribeToSceneChanges() {
    this.subscriptionManager.add('sceneChanged', this.detailsService.sceneChanged.subscribe((scene: CampaignScene) => {
        this.scene = scene;
        if (this.scene) {
          this.refreshMonsteres();
        }
    }));
  }

  private refreshMonsteres() {
    this.campaignService.getSceneCreatures(this.scene.campaignId, this.scene.id, CreatureType.Monster)
    .subscribe((monsteres: PocketCreature[]) => {
      this.monsters = monsteres as PocketMonster[];
      this.detailsService.monsters.set(monsteres);
    });
  }

  ngOnInit(): void {
  }
  ngOnDestroy(): void {
    this.subscriptionManager.clear();
  }
}
