import { Component, computed, input, signal, WritableSignal } from '@angular/core';
import {FormBuilder, FormGroup, ReactiveFormsModule} from '@angular/forms';
import {DynamicDialogConfig} from 'primeng/dynamicdialog';
import {Campaign} from '@app/campaigns/models/campaign';
import { EditorAction, EntityActionData } from '@app/models/EntityActionData';
import {createForm, getAsForm} from '@app/tokens/EditorExtension';
import {v4 as uuid} from 'uuid';
import {Fieldset} from 'primeng/fieldset';
import {InputText} from 'primeng/inputtext';
import {Textarea} from 'primeng/textarea';
import {NgIf} from '@angular/common';
import {canEdit} from '@app/tokens/utils.funcs';
import { BonusesComponent } from '@app/bonuses/bonuses/bonuses.component';
import { Bonus } from '@app/models/bonuses/bonus';
import { firstValueFrom } from 'rxjs';
import { Archetype } from '@app/models/archetypes/archetype';
import { ArchetypesService } from '@services/archetypes/archetypes.service';

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
  templateUrl: './archetype-editor.component.html',
  styleUrl: './archetype-editor.component.scss'
})
export class ArchetypeEditorComponent {
  public loaded = signal(false);
  public form: FormGroup;
  public campaign: Campaign;
  public archetype: WritableSignal<Archetype>;
  public bonuses = computed(() => this.archetype().bonuses);
  private action: EditorAction = EditorAction.create;

  public get archetypeTitle(): string {
    return this.campaign.campaignTemplate.archetypeTitle;
  }

  constructor(dialogConfig: DynamicDialogConfig,
              private service: ArchetypesService) {
    this.campaign = dialogConfig.data.campaign as Campaign;
    const creature = dialogConfig.data.archetype as Archetype;

    if (creature) {
      this.action = EditorAction.update;
    } else {
      this.action = EditorAction.create;
    }

    this.archetype = signal(creature ?? {
      id: uuid(),
      name: '',
      bonuses: [],
      description: '',
    });

    this.form = getAsForm(this.archetype());
    if (!canEdit(this.campaign)) {
      this.form.disable();
    }
    this.loaded.set(true);
  }

  async onBonusUpdated(action: EntityActionData<Bonus>) {
    switch (action.action) {
      case EditorAction.create:
        await firstValueFrom(this.service.addBonus(this.campaign.campaignTemplate.id, this.archetype().id, action.entity));
        break;
      case EditorAction.delete:
        await firstValueFrom(this.service.removeBonus(this.campaign.campaignTemplate.id, this.archetype().id, action.entity.id));
        break;
      case EditorAction.update:
        await firstValueFrom(this.service.updateBonus(this.campaign.campaignTemplate.id, this.archetype().id, action.entity));
        break;
    }
    this.archetype.set(await firstValueFrom(this.service.getById(this.campaign.campaignTemplate.id, this.archetype().id)));
  }
}

