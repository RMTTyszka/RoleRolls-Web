import { Component, ViewChild } from '@angular/core';
import { CampaignPlayer } from '@app/campaigns/models/CampaignPlayer.model';
import { CampaignScene } from '@app/campaigns/models/campaign-scene-model';
import { MenuItem } from 'primeng/api';
import { ActivatedRoute } from '@angular/router';
import { AuthenticationService } from '@app/authentication/services/authentication.service';
import { DialogService } from 'primeng/dynamicdialog';
import { forkJoin } from 'rxjs';
import { EditorAction } from '@app/models/EntityActionData';
import { SceneCreature } from '@app/campaigns/models/scene-creature.model';
import { OverlayPanel } from 'primeng/overlaypanel';
import { Campaign } from '@app/campaigns/models/campaign';
import { CreatureCategory } from '@app/campaigns/models/CreatureCategory';
import { CampaignSessionService } from '@app/campaign-session/campaign-session.service';
import { CampaignsService } from '@app/campaigns/services/campaigns.service';
import { CreatureEditorComponent } from '@app/creatures/creature-editor/creature-editor.component';
import { Creature } from '@app/campaigns/models/creature';
import { Panel } from 'primeng/panel';
import { Select } from 'primeng/select';
import { Fieldset } from 'primeng/fieldset';
import { Popover } from 'primeng/popover';
import { CdkCopyToClipboard } from '@angular/cdk/clipboard';
import { CreatureSelectComponent } from '@app/campaign-session/creature-select/creature-select.component';
import { SceneCreaturesComponent } from '@app/campaign-session/scene-creatures/scene-creatures.component';
import { SceneLogComponent } from '@app/campaign-session/scene-log/scene-log.component';
import { FormsModule } from '@angular/forms';
import { NgIf } from '@angular/common';

@Component({
  selector: 'rr-campaign-session',
  imports: [
    Panel,
    Select,
    Fieldset,
    Popover,
    CdkCopyToClipboard,
    CreatureSelectComponent,
    SceneCreaturesComponent,
    SceneLogComponent,
    FormsModule,
    NgIf
  ],
  templateUrl: './campaign-session.component.html',
  styleUrl: './campaign-session.component.scss'
})
export class CampaignSessionComponent {
  @ViewChild('invitationCodeOverlay') public invitationCodeOverlay: OverlayPanel;
  public invitationCode: string;
  public campaignId: string;
  public loadMe = false;
  public campaign: Campaign;
  public campaignPlayers: CampaignPlayer[] = [];

  public selectedScene: CampaignScene;
  public newSceneName: string;
  public scenes: CampaignScene[] = [];
  public menuItens: MenuItem[] = [];

  public isMaster: boolean;
  public notInvited: boolean;
  public creatureTypeEnum = CreatureCategory;
  constructor(
    private readonly route: ActivatedRoute,
    private readonly campaignService: CampaignsService,
    private readonly detailsService: CampaignSessionService,
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
      id: crypto.randomUUID(),
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
    this.dialogService.open(CreatureEditorComponent, {
      width: '100vw',
      height: '100vh',
      data: {
        campaign: this.campaign,
        action: EditorAction.create,
        creatureType: CreatureCategory.Hero
      },
    });
  }
  public addHero(hero: Creature){
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
