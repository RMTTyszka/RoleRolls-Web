import { Injectable, Injector } from '@angular/core';
import { Observable, of } from 'rxjs';
import { map, tap } from 'rxjs/operators';
import { LOH_API } from 'src/app/loh.api';
import { BaseCrudService } from 'src/app/shared/base-service/base-crud-service';
import { RRColumns } from 'src/app/shared/components/cm-grid/cm-grid.component';
import { PocketCampaignModel } from 'src/app/shared/models/pocket/campaigns/pocket.campaign.model';
import { AttributeTemplateModel, CreatureTemplateModel, MinorSkillsTemplateModel, SkillTemplateModel } from 'src/app/shared/models/pocket/creature-templates/creature-template.model';
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
  ) {
    super(injector);
   }

   override getNew() :Observable<PocketCampaignModel> {
      return of<PocketCampaignModel>({
        id: uuidv4(),
        name: 'New Campaign',
        creatureTemplate: new CreatureTemplateModel()
      } as PocketCampaignModel);
   }
   override get(id: string): Observable<PocketCampaignModel> {
       return super.get(id);
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
}

