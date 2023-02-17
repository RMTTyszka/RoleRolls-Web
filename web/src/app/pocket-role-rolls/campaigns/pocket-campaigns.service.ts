import { HttpParams } from '@angular/common/http';
import { Injectable, Injector } from '@angular/core';
import { Observable, of } from 'rxjs';
import { map, tap } from 'rxjs/operators';
import { AuthenticationService } from 'src/app/authentication/authentication.service';
import { LOH_API } from 'src/app/loh.api';
import { BaseCrudService } from 'src/app/shared/base-service/base-crud-service';
import { RRColumns } from 'src/app/shared/components/cm-grid/cm-grid.component';
import { PagedOutput } from 'src/app/shared/dtos/PagedOutput';
import { CreatureType } from 'src/app/shared/models/creatures/CreatureType';
import { CampaignScene } from 'src/app/shared/models/pocket/campaigns/campaign-scene-model';
import { PocketCampaignModel } from 'src/app/shared/models/pocket/campaigns/pocket.campaign.model';
import { AttributeTemplateModel, CreatureTemplateModel, LifeTemplateModel, MinorSkillsTemplateModel, SkillTemplateModel } from 'src/app/shared/models/pocket/creature-templates/creature-template.model';
import { v4 as uuidv4 } from 'uuid';

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

   public getScenes(campaignId: string): Observable<CampaignScene[]> {
    return this.http.get<CampaignScene[]>(`${this.completePath}/${campaignId}/scenes`);
  }

  public addScene(campaignId: string, newScene: CampaignScene): Observable<CampaignScene> {
    return this.http.post<never>(`${this.completePath}/${campaignId}/scenes`, newScene);
  }

  public removeScene(campaignId: string, sceneId: string): Observable<CampaignScene> {
    return this.http.delete<never>(`${this.completePath}/${campaignId}/scenes/${sceneId}`);
  }
  public getSceneCreatures(campaignId: string, sceneId: string, creatureType: CreatureType): Observable<CampaignScene[]> {
    const params = new HttpParams().set('creatureType', creatureType);
    return this.http.get<CampaignScene[]>(`${this.completePath}/${campaignId}/scenes/${sceneId}/creatures`, { params});
  }
}

