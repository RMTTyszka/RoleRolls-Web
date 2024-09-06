import {Injectable} from '@angular/core';
import {BehaviorSubject} from "rxjs";
import {PocketCampaignModel} from "src/app/shared/models/pocket/campaigns/pocket.campaign.model";
import {ItemTemplateModel} from "src/app/shared/models/pocket/itens/ItemTemplateModel";
import {EditorAction} from "src/app/shared/dtos/ModalEntityData";

@Injectable({
  providedIn: 'root'
})
export class CampaignEditorDetailsServiceService {

  public campaign = new BehaviorSubject<PocketCampaignModel>(null);
  public itemTemplate = new BehaviorSubject<ItemTemplateModel>(null);
  public itemTemplateEditorAction = new BehaviorSubject<EditorAction>(EditorAction.create);
  constructor() { }
}
