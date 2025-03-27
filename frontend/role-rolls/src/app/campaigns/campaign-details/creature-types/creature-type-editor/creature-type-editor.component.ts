import { Component, computed, input, signal, WritableSignal } from '@angular/core';
import {FormBuilder, FormGroup, ReactiveFormsModule} from '@angular/forms';
import {DynamicDialogConfig} from 'primeng/dynamicdialog';
import {Campaign} from '@app/campaigns/models/campaign';
import {CreatureType} from '@app/models/creatureTypes/creature-type';
import { EditorAction, EntityActionData } from '@app/models/EntityActionData';
import {createForm, getAsForm} from '@app/tokens/EditorExtension';
import {v4 as uuid} from 'uuid';
import {Fieldset} from 'primeng/fieldset';
import {InputText} from 'primeng/inputtext';
import {Textarea} from 'primeng/textarea';
import {NgIf} from '@angular/common';
import {canEditTemplate} from '@app/tokens/utils.funcs';
import { BonusesComponent } from '@app/bonuses/bonuses/bonuses.component';
import { CreatureTypesService } from '@services/creature-types/creature-types.service';
import { Bonus } from '@app/models/bonuses/bonus';
import { firstValueFrom } from 'rxjs';

@Component({
  selector: 'rr-archetype-editor',
  imports: [
    ReactiveFormsModule,
    Fieldset,
    InputText,
    Textarea,
    NgIf,
    BonusesComponent
  ],
  templateUrl: './creature-type-editor.component.html',
  styleUrl: './creature-type-editor.component.scss'
})
export class CreatureTypeEditorComponent {
  public loaded = signal(false);
  public form: FormGroup;
  public campaign: Campaign;
  public creatureType: WritableSignal<CreatureType>;
  public bonuses = computed(() => this.creatureType().bonuses);
  private action: EditorAction = EditorAction.create;

  public get creatureTypeTitle(): string {
    return this.campaign.campaignTemplate.creatureTypeTitle;
  }

  constructor(private dialogConfig: DynamicDialogConfig,
              private service: CreatureTypesService) {
    this.campaign = dialogConfig.data.campaign as Campaign;
    const creature = dialogConfig.data.creatureType as CreatureType;

    if (creature) {
      this.action = EditorAction.update;
    } else {
      this.action = EditorAction.create;
    }

    this.creatureType = signal(creature ?? {
      id: uuid(),
      name: '',
      bonuses: [],
      description: '',
    });

    this.form = getAsForm(this.creatureType());
    if (!canEditTemplate(this.campaign)) {
      this.form.disable();
    }
    this.loaded.set(true);
  }

  async onBonusUpdated(action: EntityActionData<Bonus>) {
    switch (action.action) {
      case EditorAction.create:
        await firstValueFrom(this.service.addBonus(this.campaign.campaignTemplate.id, this.creatureType().id, action.entity));
        break;
      case EditorAction.delete:
        await firstValueFrom(this.service.removeBonus(this.campaign.campaignTemplate.id, this.creatureType().id, action.entity.id));
        break;
      case EditorAction.update:
        await firstValueFrom(this.service.updateBonus(this.campaign.campaignTemplate.id, this.creatureType().id, action.entity));
        break;
    }
    this.creatureType.set(await firstValueFrom(this.service.getById(this.campaign.campaignTemplate.id, this.creatureType().id)));
  }
}

