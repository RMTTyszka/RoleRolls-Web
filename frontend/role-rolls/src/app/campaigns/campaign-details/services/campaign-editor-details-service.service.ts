import {Injectable} from '@angular/core';
import {BehaviorSubject} from "rxjs";
import { EditorAction } from '@app/models/EntityActionData';
import { Campaign } from '../../models/campaign';
import { ItemTemplateModel } from '@app/models/itens/ItemTemplateModel';

@Injectable({
  providedIn: 'root'
})
export class CampaignEditorDetailsServiceService {

  public campaign = new BehaviorSubject<Campaign>(null);
  public itemTemplate = new BehaviorSubject<ItemTemplateModel>(null);
  public itemTemplateEditorAction = new BehaviorSubject<EditorAction>(EditorAction.create);
  constructor() { }
}
