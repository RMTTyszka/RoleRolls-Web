import { Component, computed, input } from '@angular/core';
import { Archetype, ArchetypePowerDescription } from '@app/models/archetypes/archetype';
import { Fieldset } from 'primeng/fieldset';
import { AbstractControl, FormArray, FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { getAsForm } from '@app/tokens/EditorExtension';
import { JsonPipe } from '@angular/common';
import { FloatLabel } from 'primeng/floatlabel';
import { Textarea } from 'primeng/textarea';
import { SubscriptionManager } from '@app/tokens/subscription-manager';
import { ArchetypesService } from '@services/archetypes/archetypes.service';
import {Campaign} from '@app/campaigns/models/campaign';
import { Editor } from 'primeng/editor';
import { Button, ButtonDirective } from 'primeng/button';
import { MarkdownViewerComponent } from '@app/shared/components/markdown-viewer/markdown-viewer.component';

@Component({
  selector: 'rr-archetype-power-descriptions',
  imports: [
    Fieldset,
    ReactiveFormsModule,
    FloatLabel,
    Textarea,
    Editor,
    MarkdownViewerComponent,
    ButtonDirective
  ],
  templateUrl: './archetype-power-descriptions.component.html',
  styleUrl: './archetype-power-descriptions.component.scss'
})
export class ArchetypePowerDescriptionsComponent {

  public archetype = input<Archetype>();
  public campaign = input<Campaign>();
  public powers = computed<FormArray>(() => {
    return this.formBuilder.array(this.archetype().powerDescriptions.sort((a, b) => {
      if (a.level < b.level) return -1;
      if (a.level > b.level) return 1;
      if (a.name < b.name) return -1;
      if (a.name > b.name) return 1;
      return 0;
    }).map(e => {
      const form = getAsForm(e)
      form.addControl('title', this.formBuilder.control(`Level ${e.level} - ${e.name}`))
      this.subscriptionManager.add(e.id, form.valueChanges.subscribe((v: ArchetypePowerDescription) => {
        this.service.updatePowerDescription(this.campaign().campaignTemplateId, this.archetype().id, v)
      }))
      return form;
    }));
  });
  public form = computed(() => {
    return this.formBuilder.group({
      powers: this.powers()
    });
  });
  private subscriptionManager = new SubscriptionManager()
  private editingIds = new Set<string>();
  constructor(
    private formBuilder: FormBuilder,
    private service: ArchetypesService,
  ) {

  }
  ngOnInit() {

  }

  protected readonly FormGroup = FormGroup;
  isEditing(index: number): boolean {
    const id = this.getPowerId(index);
    return this.editingIds.has(id);
  }

  toggleEdit(index: number, force?: boolean): void {
    const id = this.getPowerId(index);
    const shouldEdit = force ?? !this.editingIds.has(id);
    if (shouldEdit) {
      this.editingIds.add(id);
    } else {
      this.editingIds.delete(id);
    }
  }

  private getPowerId(index: number): string {
    const ctrl = this.powers().at(index) as AbstractControl;
    const val = ctrl?.value as { id?: string };
    return val?.id ?? String(index);
  }
}
