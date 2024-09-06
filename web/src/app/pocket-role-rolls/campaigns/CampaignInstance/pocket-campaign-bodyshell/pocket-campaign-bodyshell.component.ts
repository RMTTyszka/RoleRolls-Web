import { Component, OnInit, TemplateRef, ViewChild, ViewChildren, ChangeDetectorRef } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MenuItem } from 'primeng/api';
import { forkJoin } from 'rxjs';
import { CampaignScene } from 'src/app/shared/models/pocket/campaigns/campaign-scene-model';
import { PocketCampaignModel } from 'src/app/shared/models/pocket/campaigns/pocket.campaign.model';
import { PocketCampaignDetailsService } from './pocket-campaign-details.service';
import { v4 as uuidv4 } from 'uuid';
import { AuthenticationService } from 'src/app/authentication/authentication.service';
import { OverlayPanel } from 'primeng/overlaypanel';
import { CampaignPlayer } from 'src/app/shared/models/pocket/campaigns/CampaignPlayer.model';
import { DialogService } from 'primeng/dynamicdialog';
import { EditorAction } from 'src/app/shared/dtos/ModalEntityData';
import { PocketCreature } from 'src/app/shared/models/pocket/creatures/pocket-creature';
import { SceneCreature } from 'src/app/shared/models/pocket/campaigns/scene-creature.model';
import { CreatureType } from 'src/app/shared/models/creatures/CreatureType';
import {PocketCampaignsService} from "../../pocket-campaigns.service";
import {PocketCreatureEditorComponent} from "../../../pocket-creature-editor/pocket-creature-editor.component";

@Component({
  selector: 'rr-pocket-campaign-bodyshell',
  templateUrl: './pocket-campaign-bodyshell.component.html',
  styleUrls: ['./pocket-campaign-bodyshell.component.scss'],
  providers: [DialogService]
})
export class PocketCampaignBodyshellComponent implements OnInit {
  @ViewChild('invitationCodeOverlay') public invitationCodeOverlay: OverlayPanel;
  public invitationCode: string;
  public campaignId: string;
  public loadMe = false;
  public campaign: PocketCampaignModel = new PocketCampaignModel();
  public campaignPlayers: CampaignPlayer[] = [];

  public selectedScene: CampaignScene;
  public newSceneName: string;
  public scenes: CampaignScene[] = [];
  public menuItens: MenuItem[] = [];

  public isMaster: boolean;
  public notInvited: boolean;
  public creatureTypeEnum = CreatureType;
  constructor(
    private readonly route: ActivatedRoute,
    private readonly campaignService: PocketCampaignsService,
    private readonly detailsService: PocketCampaignDetailsService,
    private readonly authenticationService: AuthenticationService,
    private readonly dialogService: DialogService,
  ) {
    this.campaignId = this.route.snapshot.params['id'];
    forkJoin({
      campaign: this.campaignService.get(this.campaignId),
      scenes: this.campaignService.getScenes(this.campaignId),
      players: this.campaignService.getPlayers(this.campaignId)
    })
    .subscribe((result) => {
      this.campaign = result.campaign;
      this.detailsService.campaignLoaded.next(this.campaign);
      this.scenes = result.scenes;
      this.campaignPlayers = result.players;
      if (this.scenes.length > 0) {
        const currentScene = this.detailsService.currentScene;
        if (currentScene) {
          const currentSceneIndex = this.scenes.findIndex(s => s.id == currentScene.id);
          this.selectScene(this.scenes[currentSceneIndex]);
        } else {
          this.selectScene(this.scenes[0]);
        }
      }
      this.isMaster = this.authenticationService.userId === this.campaign.masterId;
      this.notInvited = !this.campaignPlayers.some(c => c.playerId === this.authenticationService.userId);
    });
    this.menuItens = [
      {
        icon: 'fist-raised'
      } as MenuItem,
    ];
    setTimeout(() => {
      this.loadMe = true;
    }, 500)
   }

  ngOnInit(): void {
  }
  public newScene() {
    const newScene = {
      id: uuidv4(),
      name: this.newSceneName,
      campaignId: this.campaignId
    } as CampaignScene;
    this.campaignService.addScene(this.campaignId, newScene)
    .subscribe(() => {
      this.newSceneName = '';
      this.scenes.push(newScene);
      this.selectScene(newScene);
    });
  }
  public selectScene(scene: CampaignScene) {
    this.selectedScene = scene;
    this.detailsService.sceneChanged.next(this.selectedScene);
  }

  public invitePlayer(target: any){
    this.campaignService.invitePlayer(this.campaign.id).subscribe((code: string) => {
      this.invitationCode = code;
      this.invitationCodeOverlay.toggle(true, target);
    });
  }

  public createHero(){
    this.dialogService.open(PocketCreatureEditorComponent, {
      width: '100vw',
      height: '100vh',
      data: {
        campaign: this.campaign,
        action: EditorAction.create,
        creatureType: CreatureType.Hero
      },
    });
  }
  public addHero(hero: PocketCreature){
    const input = {
      creatureId: hero.id,
      hidden: false
    } as SceneCreature;
    this.campaignService.addHeroToScene(this.campaignId, this.selectedScene.id, input)
    .subscribe(() => {
      this.detailsService.heroAddedToScene.next();
    });
  }
}
