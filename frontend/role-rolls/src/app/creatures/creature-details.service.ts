import { Injectable } from '@angular/core';
import {Observable, Subject} from 'rxjs';
import {DialogService} from 'primeng/dynamicdialog';
import {Creature} from '@app/campaigns/models/creature';
import {CreatureEditorComponent} from '@app/creatures/creature-editor/creature-editor.component';
import {EditorAction} from '@app/models/EntityActionData';
import {CreatureCategory} from '@app/campaigns/models/CreatureCategory';
import {Campaign} from '@app/campaigns/models/campaign';

@Injectable({
  providedIn: 'root'
})
export class CreatureDetailsService {

  public refreshCreature = new Subject<void>();
  public debug = new Subject<void>();
  constructor(private dialog: DialogService) { }
  public openCreatureEditor(campaign: Campaign, id: string, creatureCategory: CreatureCategory, action: EditorAction): Observable<Creature> {
    return this.dialog.open(CreatureEditorComponent, {
      width: '100vw',
      height: '100vh',
      data: {
        campaign: campaign,
        action: action,
        creatureId: id,
        creatureType: creatureCategory,
      },
      focusOnShow: false,
      closable: true,
    }).onClose
  }
}
