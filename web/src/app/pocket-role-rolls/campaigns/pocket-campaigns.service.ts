import {HttpParams, HttpResponse} from '@angular/common/http';
import {Injectable, Injector} from '@angular/core';
import {Observable, of} from 'rxjs';
import {switchMap} from 'rxjs/operators';
import {AuthenticationService} from 'src/app/authentication/authentication.service';
import {LOH_API} from 'src/app/loh.api';
import {BaseCrudService} from 'src/app/shared/base-service/base-crud-service';
import {RRColumns} from 'src/app/shared/components/cm-grid/cm-grid.component';
import {PagedOutput} from 'src/app/shared/dtos/PagedOutput';
import {CreatureType} from 'src/app/shared/models/creatures/CreatureType';
import {AcceptInvitationInput} from 'src/app/shared/models/pocket/campaigns/accept-invitation-input';
import {CampaignScene} from 'src/app/shared/models/pocket/campaigns/campaign-scene-model';
import {CampaignPlayer} from 'src/app/shared/models/pocket/campaigns/CampaignPlayer.model';
import {PocketCampaignModel} from 'src/app/shared/models/pocket/campaigns/pocket.campaign.model';
import {SceneCreature} from 'src/app/shared/models/pocket/campaigns/scene-creature.model';
import {
  AttributeTemplateModel,
  CreatureTemplateModel,
  DefenseTemplateModel,
  LifeTemplateModel,
  MinorSkillsTemplateModel,
  SkillTemplateModel
} from 'src/app/shared/models/pocket/creature-templates/creature-template.model';
import {PocketCreature} from 'src/app/shared/models/pocket/creatures/pocket-creature';
import {v4 as uuidv4} from 'uuid';
import {PocketRoll} from './models/pocket-roll.model';
import {RollInput} from './models/RollInput';
import {TakeDamageApiInput} from 'src/app/pocket-role-rolls/campaigns/models/TakeDamageApiInput';
import {SimulateCdInput} from './models/SimulateCdInput';
import {SimulateCdResult} from './models/simulate-cd-result';
import {AttackInput} from 'src/app/pocket-role-rolls/campaigns/models/TakeDamangeInput';

@Injectable({
  providedIn: 'root'
})
export class PocketCampaignsService extends BaseCrudService<PocketCampaignModel, PocketCampaignModel> {
  public path = 'campaigns';
  public selectPlaceholder: string;
  public fieldName: string;
  public selectModalTitle: string;
  public selectModalColumns: RRColumns[];
  public entityListColumns: RRColumns[];
  public serverUrl = LOH_API.myPocketBackUrl;
  constructor(
    injector: Injector,
    private authenticationService: AuthenticationService,
  ) {
    super(injector);
   }

   override getNew() :Observable<PocketCampaignModel> {
    const campaignModel = {
      id: uuidv4(),
      name: 'New Campaign',
      creatureTemplate: new CreatureTemplateModel(),
      masterId: this.authenticationService.userId
    } as PocketCampaignModel;
      return of<PocketCampaignModel>(campaignModel);
   }
   override get(id: string): Observable<PocketCampaignModel> {
       return super.get(id);
   }

   override beforeCreate(entity: PocketCampaignModel): PocketCampaignModel {
      entity.creatureTemplate = null;
      return entity;
   }
   public addAttribute(campaignId: string, attribute: AttributeTemplateModel): Observable<never> {
    return this.http.post<never>(`${this.completePath}/${campaignId}/attributes`, attribute);
   }
   public updateAttribute(campaignId: string, attributeId: string, attribute: AttributeTemplateModel): Observable<never> {
    return this.http.put<never>(`${this.completePath}/${campaignId}/attributes/${attributeId}`, attribute);
   }
   public removeAttribute(campaignId: string, attributeId: string): Observable<never> {
    return this.http.delete<never>(`${this.completePath}/${campaignId}/attributes/${attributeId}`);
   }

   public addSkill(campaignId: string, attributeId: string, skill: SkillTemplateModel): Observable<never> {
    return this.http.post<never>(`${this.completePath}/${campaignId}/attributes/${attributeId}/skills`, skill);
   }
   public updateSkill(campaignId: string, attributeId: string, skillId: string, skill: SkillTemplateModel): Observable<never> {
    return this.http.put<never>(`${this.completePath}/${campaignId}/attributes/${attributeId}/skills/${skillId}`, skill);
   }
   public removeSkill(campaignId: string, attributeId: string, skillId: string): Observable<never> {
    return this.http.delete<never>(`${this.completePath}/${campaignId}/attributes/${attributeId}/skills/${skillId}`);
   }


   public addMinorSkill(campaignId: string, attributeId: string, skillId: string, minorSkill: MinorSkillsTemplateModel): Observable<never> {
    return this.http.post<never>(`${this.completePath}/${campaignId}/attributes/${attributeId}/skills/${skillId}/minor-skills`, minorSkill);
  }

   public updateMinorSkill(campaignId: string, attributeId: string, skillId: string, minorSkillId: string, minorSkill: MinorSkillsTemplateModel): Observable<never> {
    return this.http.put<never>(`${this.completePath}/${campaignId}/attributes/${attributeId}/skills/${skillId}/minor-skills/${minorSkillId}`, minorSkill);
  }
   public removeMinorSkill(campaignId: string, attributeId: string, skillId: string, minorSkillId: string): Observable<never> {
    return this.http.delete<never>(`${this.completePath}/${campaignId}/attributes/${attributeId}/skills/${skillId}/minor-skills/${minorSkillId}`);
  }

  public addLife(campaignId: string, life: LifeTemplateModel): Observable<never> {
    return this.http.post<never>(`${this.completePath}/${campaignId}/lifes`, life);
   }
   public updateLife(campaignId: string, lifeId: string, life: LifeTemplateModel): Observable<never> {
    return this.http.put<never>(`${this.completePath}/${campaignId}/lifes/${lifeId}`, life);
   }
   public removeLife(campaignId: string, lifeId: string): Observable<never> {
    return this.http.delete<never>(`${this.completePath}/${campaignId}/lifes/${lifeId}`);
   }

   public addDefense(campaignId: string, defense: DefenseTemplateModel) {
    return this.http.post<never>(`${this.completePath}/${campaignId}/defenses`, defense);
  }
  public updateDefense(campaignId: string, defenseId: string, defense: DefenseTemplateModel): Observable<never> {
    return this.http.put<never>(`${this.completePath}/${campaignId}/defenses/${defenseId}`, defense);
   }
   public removeDefense(campaignId: string, defenseId: string): Observable<never> {
    return this.http.delete<never>(`${this.completePath}/${campaignId}/defenses/${defenseId}`);
   }



   public getCreatureTemplate(id: string): Observable<CreatureTemplateModel> {
    return this.http.get<CreatureTemplateModel>(`${this.serverUrl}creature-templates/${id}`);
  }
  public getCreatures(campaignId: string, creatureType: CreatureType): Observable<PocketCreature[]> {
    const params = new HttpParams().set('creatureType', creatureType)
    return this.http.get<PocketCreature[]>(`${this.completePath}/${campaignId}/creatures`, {
      params
    });
  }
  public getCreature(campaignId: string, creatureId: string): Observable<PocketCreature> {
    return this.http.get<PocketCreature>(`${this.completePath}/${campaignId}/creatures/${creatureId}`);
  }
  public instantiateNewCreature(campaignId: string, creatureType: CreatureType):  Observable<PocketCreature> {
    const params = new HttpParams().set('creatureType', creatureType)
    return this.http.get<PocketCreature>(`${this.completePath}/${campaignId}/creatures/new`, {
      params
    }, );
  }
  public createCreature(campaignId: string, creature: PocketCreature) {
    return this.http.post<never>(`${this.completePath}/${campaignId}/creatures`, creature);
  }
  public updateCreature(campaignId: string, creature: PocketCreature) {
    return this.http.put<never>(`${this.completePath}/${campaignId}/creatures/${creature.id}`, creature);
  }
  public takeDamage(campaignId: string, sceneId: string, creatureId: string, input: TakeDamageApiInput) {
    return this.http.post<never>(`${this.completePath}/${campaignId}/scenes/${sceneId}/creatures/${creatureId}/damage`, input);
  }
  public heal(campaignId: string, sceneId: string, creatureId: string, input: TakeDamageApiInput) {
    return this.http.post<never>(`${this.completePath}/${campaignId}/scenes/${sceneId}/creatures/${creatureId}/heal`, input);
  }
  public getScenes(campaignId: string): Observable<CampaignScene[]> {
    return this.http.get<CampaignScene[]>(`${this.completePath}/${campaignId}/scenes`);
  }

  public addScene(campaignId: string, newScene: CampaignScene): Observable<CampaignScene> {
    return this.http.post<never>(`${this.completePath}/${campaignId}/scenes`, newScene);
  }

  public removeScene(campaignId: string, sceneId: string): Observable<CampaignScene> {
    return this.http.delete<never>(`${this.completePath}/${campaignId}/scenes/${sceneId}`);
  }
  public getSceneCreatures(campaignId: string, sceneId: string, creatureType: CreatureType): Observable<PocketCreature[]> {
    const params = new HttpParams().set('creatureType', creatureType);
    return this.http.get<PocketCreature[]>(`${this.completePath}/${campaignId}/scenes/${sceneId}/creatures`, { params});
  }
  public addHeroToScene(campaignId: string, sceneId: string, input: SceneCreature) {
    input.creatureType = CreatureType.Hero;
    return this.http.post<never>(`${this.completePath}/${campaignId}/scenes/${sceneId}/creatures`, [input]);
  }
  public addMonsterToScene(campaignId: string, sceneId: string, input: SceneCreature) {
    input.creatureType = CreatureType.Monster;
    return this.http.post<never>(`${this.completePath}/${campaignId}/scenes/${sceneId}/creatures`, [input]);
  }
  public removeCreatureFromScene(campaignId: string, sceneId: string, creatureId: string) {
    return this.http.delete<never>(`${this.completePath}/${campaignId}/scenes/${sceneId}/creatures/${creatureId}`);
  }

  public simulateCd(campaignId: string, sceneId: string, input: SimulateCdInput): Observable<SimulateCdResult[]> {
    return this.http.post<SimulateCdResult[]>(`${this.completePath}/${campaignId}/scenes/${sceneId}/creatures/${input.creatureId}/roll-simulations`, input)
  }
  public rollForCreature(campaignId: string, sceneId: string, creatureId: string, rollInput: RollInput) {
    return this.http.post<never>(`${this.completePath}/${campaignId}/scenes/${sceneId}/creatures/${creatureId}/rolls`, rollInput, {observe: 'response'})
    .pipe(
      switchMap((response: HttpResponse<never>) => {
        const location = response.headers.get('Location')
        return this.http.get<PocketRoll>(location)
      }))
  }

  public getPlayers(campaignId: string): Observable<CampaignPlayer[]> {
    return this.http.get<CampaignPlayer[]>(`${this.completePath}/${campaignId}/players`, {});
  }
  public invitePlayer(campaignId: string): Observable<string> {
    return this.http.post<string>(`${this.completePath}/${campaignId}/players`, {});
  }
  public acceptInvitation(invitationCode: string): Observable<never> {
    const input = {
      invitationCode: invitationCode
    } as AcceptInvitationInput;
    return this.http.put<never>(`${this.completePath}/invitations`, input);
  }


  public getSceneRolls(campaignId: string, sceneId: string, skipCount: number, maxResultCount: number) {
    const params = new HttpParams().set('skipCount', skipCount).set('maxResultCount', maxResultCount);
    return this.http.get<PagedOutput<PocketRoll>>(`${this.completePath}/${campaignId}/scenes/${sceneId}/rolls`, {params});
  }

  public attack(campaignId: string, sceneId: string, creatureId: string, input: AttackInput) {
    return this.http.post<never>(`${this.completePath}/${campaignId}/scenes/${sceneId}/creatures/${creatureId}/attacks`, input);
  }
}

