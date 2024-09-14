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
import {HttpClient, HttpParams, HttpResponse} from "@angular/common/http";
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
import {
  ArmorTemplateModel,
  ItemTemplateModel,
  WeaponTemplateModel
} from "../../shared/models/pocket/itens/ItemTemplateModel";

@Injectable({
  providedIn: 'root'
})
export class CampaignItemTemplatesService {
  public path = 'item-templates';
  public selectPlaceholder: string;
  public fieldName: string;
  public selectModalTitle: string;
  public selectModalColumns: RRColumns[];
  public entityListColumns: RRColumns[];
  public serverUrl = LOH_API.myPocketBackUrl;
  private get completePath(): string {
    return this.serverUrl + this.path;
  }
  constructor(
    private httpClient: HttpClient,
    private authenticationService: AuthenticationService,
  ) {
  }

  public getNew() :Observable<ItemTemplateModel> {
    const campaignModel = {
      id: uuidv4(),
      name: 'New Campaign',
    } as ItemTemplateModel;
    return of<ItemTemplateModel>(campaignModel);
  }
   get(itemId: string): Observable<ItemTemplateModel> {
    return this.httpClient.get<ItemTemplateModel>(`${this.completePath}/${itemId}`);
  }

  public addItem(item: ItemTemplateModel): Observable<never> {
    return this.httpClient.post<never>(`${this.completePath}`, item);
  }
  public updateItem(itemId: string, item: ItemTemplateModel): Observable<never> {
    return this.httpClient.put<never>(`${this.completePath}/${itemId}`, item);
  }
  public removeItem(itemId: string): Observable<never> {
    return this.httpClient.delete<never>(`${this.completePath}/${itemId}`);
  }

  list(campaingId: string, filter: string, skipCount: number, maxResultCount: number) {
    let params = new HttpParams()
      .set('filter', filter)
      .set('skipCount', skipCount)
      .set('maxResultCount', maxResultCount)
      .set('campaignId', campaingId);
    return this.httpClient.get<PagedOutput<ItemTemplateModel>>(`${this.completePath}`, {
      params
    });
  }

  addWeapon(weaponTemplate: WeaponTemplateModel) {
    return this.httpClient.post<never>(`${this.serverUrl}weapon-templates`, weaponTemplate);
  }

  updateWeapon(id: string, weaponTemplate: WeaponTemplateModel) {
    return this.httpClient.put<never>(`${this.serverUrl}weapon-templates/${id}`, weaponTemplate);
  }

  addArmor(template: ArmorTemplateModel) {
    return this.httpClient.post<never>(`${this.serverUrl}armor-templates`, template);
  }
  updateArmor(id: string, template: ArmorTemplateModel) {
    return this.httpClient.put<never>(`${this.serverUrl}armor-templates/${id}`, template);
  }
}
