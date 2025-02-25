import {Injectable} from '@angular/core';
import {BehaviorSubject} from "rxjs";
import { ItemTemplateModel } from '../../../models/ItemTemplateModel';
import { EditorAction } from '../../../models/EntityActionData';
import { Campaign } from '../../models/campaign';

@Injectable({
  providedIn: 'root'
})
export class CampaignEditorDetailsServiceService {

  public campaign = new BehaviorSubject<Campaign>(null);
  public itemTemplate = new BehaviorSubject<ItemTemplateModel>(null);
  public itemTemplateEditorAction = new BehaviorSubject<EditorAction>(EditorAction.create);
  constructor() { }
}
