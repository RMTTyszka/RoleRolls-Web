import {Injectable, Injector} from '@angular/core';
import {BaseCrudService} from "../../shared/base-service/base-crud-service";
import {PocketCampaignModel} from "../../shared/models/pocket/campaigns/pocket.campaign.model";
import {RRColumns} from "../../shared/components/cm-grid/cm-grid.component";
import {LOH_API} from "../../loh.api";
import {AuthenticationService} from "../../authentication/authentication.service";
import {Observable, of} from "rxjs";
import {
  AttributeTemplateModel,
  CreatureTemplateModel, DefenseTemplateModel, LifeTemplateModel, MinorSkillsTemplateModel, SkillTemplateModel
} from "../../shared/models/pocket/creature-templates/creature-template.model";
import {CreatureType} from "../../shared/models/creatures/CreatureType";
import {PocketCreature} from "../../shared/models/pocket/creatures/pocket-creature";
import {HttpParams, HttpResponse} from "@angular/common/http";
import {TakeDamageApiInput} from "../campaigns/models/TakeDamageApiInput";
import {CampaignScene} from "../../shared/models/pocket/campaigns/campaign-scene-model";
import {SceneCreature} from "../../shared/models/pocket/campaigns/scene-creature.model";
import {SimulateCdInput} from "../campaigns/models/SimulateCdInput";
import {SimulateCdResult} from "../campaigns/models/simulate-cd-result";
import {RollInput} from "../campaigns/models/RollInput";
import {switchMap} from "rxjs/operators";
import {PocketRoll} from "../campaigns/models/pocket-roll.model";
import {CampaignPlayer} from "../../shared/models/pocket/campaigns/CampaignPlayer.model";
import {AcceptInvitationInput} from "../../shared/models/pocket/campaigns/accept-invitation-input";
import {PagedOutput} from "../../shared/dtos/PagedOutput";
import { v4 as uuidv4 } from 'uuid';
import {ItemTemplateModel} from "../../shared/models/pocket/itens/ItemTemplateModel";

@Injectable({
  providedIn: 'root'
})
export class CampaignItemTemplatesService extends BaseCrudService<ItemTemplateModel, ItemTemplateModel> {
  public path = 'item-templates';
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

  override getNew() :Observable<ItemTemplateModel> {
    const campaignModel = {
      id: uuidv4(),
      name: 'New Campaign',
    } as ItemTemplateModel;
    return of<ItemTemplateModel>(campaignModel);
  }
  override get(id: string): Observable<ItemTemplateModel> {
    return super.get(id);
  }

  override beforeCreate(entity: ItemTemplateModel): ItemTemplateModel {
    return entity;
  }
  public addItem(campaignId: string, item: ItemTemplateModel): Observable<never> {
    return this.http.post<never>(`${this.completePath}/${campaignId}/attributes`, item);
  }
  public updateItem(campaignId: string, itemId: string, item: ItemTemplateModel): Observable<never> {
    return this.http.put<never>(`${this.completePath}/${campaignId}/attributes/${itemId}`, item);
  }
  public removeItem(campaignId: string, itemId: string): Observable<never> {
    return this.http.delete<never>(`${this.completePath}/${campaignId}/attributes/${itemId}`);
  }
}
