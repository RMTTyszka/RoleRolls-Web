import {HttpClient, HttpParams, HttpResponse } from '@angular/common/http';
import {Injectable, Injector} from '@angular/core';
import {Observable, of} from 'rxjs';
import {switchMap} from 'rxjs/operators';
import { AuthenticationService } from '../../authentication/services/authentication.service';
import { Campaign } from '../models/campaign';
import {v4 as uuidv4} from 'uuid';
import { CreatureType } from '../models/CreatureType';
import { Creature } from 'app/campaigns/models/creature';
import { TakeDamageApiInput } from '../models/TakeDamageApiInput';
import { CampaignScene } from '../models/campaign-scene-model';
import { SceneCreature } from '../models/scene-creature.model';
import { SimulateCdInput } from '../models/SimulateCdInput';
import { SimulateCdResult } from '../models/simulate-cd-result';
import { RollInput } from '../models/RollInput';
import { Roll } from '../models/pocket-roll.model';
import { CampaignPlayer } from '../models/CampaignPlayer.model';
import { AcceptInvitationInput } from '../models/accept-invitation-input';
import { PagedOutput } from '../../models/PagedOutput';
import { AttackInput } from '../models/TakeDamangeInput';
import { TemplateImportInput } from '../models/TemplateImportInput';
import { safeCast } from '../../tokens/utils.funcs';
import { ItemConfigurationModel } from '../models/item-configuration-model';
import { BaseCrudService } from '../../services/base-service/base-crud-service';
import {
  AttributeTemplate,
  CampaignTemplate,
  DefenseTemplate,
  LifeTemplate,
  MinorSkillsTemplate,
  SkillTemplate
} from 'app/campaigns/models/campaign.template';

@Injectable({
  providedIn: 'root'
})
export class CampaignsService extends BaseCrudService<Campaign>{
  public path = 'campaigns';
  constructor(
    http: HttpClient,
    private authenticationService: AuthenticationService,
  ) {
    super(http)
   }

   public getNew(): Observable<Campaign> {
    const campaignModel = {
      id: uuidv4(),
      name: 'New Campaign',
      campaignTemplate: new CampaignTemplate(),
      masterId: safeCast<string>(this.authenticationService.userId),
      copy: false,
      campaignTemplateId: null,
      itemConfiguration: new ItemConfigurationModel()
    } as Campaign;
      return of<Campaign>(campaignModel);
   }

   public addAttribute(campaignId: string, attribute: AttributeTemplate): Observable<never> {
    return this.http.post<never>(`${this.completePath}/${campaignId}/attributes`, attribute);
   }
   public updateAttribute(campaignId: string, attributeId: string, attribute: AttributeTemplate): Observable<never> {
    return this.http.put<never>(`${this.completePath}/${campaignId}/attributes/${attributeId}`, attribute);
   }
   public removeAttribute(campaignId: string, attributeId: string): Observable<never> {
    return this.http.delete<never>(`${this.completePath}/${campaignId}/attributes/${attributeId}`);
   }

   public addSkill(campaignId: string, attributeId: string, skill: SkillTemplate): Observable<never> {
    return this.http.post<never>(`${this.completePath}/${campaignId}/attributes/${attributeId}/skills`, skill);
   }
   public addAttributelessSkill(campaignId: string, skill: SkillTemplate): Observable<never> {
    return this.http.post<never>(`${this.completePath}/${campaignId}/attributeless-skills`, skill);
   }
   public updateAttributelessSkill(campaignId: string, skill: SkillTemplate): Observable<never> {
    return this.http.put<never>(`${this.completePath}/${campaignId}/attributeless-skills/${skill.id}`, skill);
   }
   public removeAttributelessSkill(campaignId: string, skill: SkillTemplate): Observable<never> {
    return this.http.delete<never>(`${this.completePath}/${campaignId}/attributeless-skills/${skill.id}`);
   }
   public updateSkill(campaignId: string, attributeId: string, skillId: string, skill: SkillTemplate): Observable<never> {
    return this.http.put<never>(`${this.completePath}/${campaignId}/attributes/${attributeId}/skills/${skillId}`, skill);
   }
   public removeSkill(campaignId: string, attributeId: string, skillId: string): Observable<never> {
    return this.http.delete<never>(`${this.completePath}/${campaignId}/attributes/${attributeId}/skills/${skillId}`);
   }


   public addMinorSkill(campaignId: string, attributeId: string, skillId: string, minorSkill: MinorSkillsTemplate): Observable<never> {
    return this.http.post<never>(`${this.completePath}/${campaignId}/attributes/${attributeId}/skills/${skillId}/minor-skills`, minorSkill);
  }

   public updateMinorSkill(campaignId: string, attributeId: string, skillId: string, minorSkillId: string, minorSkill: MinorSkillsTemplate): Observable<never> {
    return this.http.put<never>(`${this.completePath}/${campaignId}/attributes/${attributeId}/skills/${skillId}/minor-skills/${minorSkillId}`, minorSkill);
  }
   public removeMinorSkill(campaignId: string, attributeId: string, skillId: string, minorSkillId: string): Observable<never> {
    return this.http.delete<never>(`${this.completePath}/${campaignId}/attributes/${attributeId}/skills/${skillId}/minor-skills/${minorSkillId}`);
  }

  public addLife(campaignId: string, life: LifeTemplate): Observable<never> {
    return this.http.post<never>(`${this.completePath}/${campaignId}/lifes`, life);
   }
   public updateLife(campaignId: string, lifeId: string, life: LifeTemplate): Observable<never> {
    return this.http.put<never>(`${this.completePath}/${campaignId}/lifes/${lifeId}`, life);
   }
   public removeLife(campaignId: string, lifeId: string): Observable<never> {
    return this.http.delete<never>(`${this.completePath}/${campaignId}/lifes/${lifeId}`);
   }

   public addDefense(campaignId: string, defense: DefenseTemplate) {
    return this.http.post<never>(`${this.completePath}/${campaignId}/defenses`, defense);
  }
  public updateDefense(campaignId: string, defenseId: string, defense: DefenseTemplate): Observable<never> {
    return this.http.put<never>(`${this.completePath}/${campaignId}/defenses/${defenseId}`, defense);
   }
   public removeDefense(campaignId: string, defenseId: string): Observable<never> {
    return this.http.delete<never>(`${this.completePath}/${campaignId}/defenses/${defenseId}`);
   }



   public getCreatureTemplate(id: string): Observable<CampaignTemplate> {
    return this.http.get<CampaignTemplate>(`${this.serverUrl}creature-templates/${id}`);
  }
  public getCreatures(campaignId: string, creatureType: CreatureType): Observable<Creature[]> {
    const params = new HttpParams().set('creatureType', creatureType)
    return this.http.get<Creature[]>(`${this.completePath}/${campaignId}/creatures`, {
      params
    });
  }
  public getCreature(campaignId: string, creatureId: string): Observable<Creature> {
    return this.http.get<Creature>(`${this.completePath}/${campaignId}/creatures/${creatureId}`);
  }
  public instantiateNewCreature(campaignId: string, creatureType: CreatureType):  Observable<Creature> {
    const params = new HttpParams().set('creatureType', creatureType)
    return this.http.get<Creature>(`${this.completePath}/${campaignId}/creatures/new`, {
      params
    }, );
  }
  public createCreature(campaignId: string, creature: Creature) {
    return this.http.post<never>(`${this.completePath}/${campaignId}/creatures`, creature);
  }
  public updateCreature(campaignId: string, creature: Creature) {
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
  public getSceneCreatures(campaignId: string, sceneId: string, creatureType: CreatureType): Observable<Creature[]> {
    const params = new HttpParams().set('creatureType', creatureType);
    return this.http.get<Creature[]>(`${this.completePath}/${campaignId}/scenes/${sceneId}/creatures`, { params});
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
        return this.http.get<Roll>(safeCast<string>(location))
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
    return this.http.get<PagedOutput<Roll>>(`${this.completePath}/${campaignId}/scenes/${sceneId}/rolls`, {params});
  }

  public attack(campaignId: string, sceneId: string, creatureId: string, input: AttackInput) {
    return this.http.post<never>(`${this.completePath}/${campaignId}/scenes/${sceneId}/creatures/${creatureId}/attacks`, input);
  }

  importTemplate(template: TemplateImportInput) {
    return this.http.post<never>(`${this.completePath}/campaigns/import`, template);
  }
}

