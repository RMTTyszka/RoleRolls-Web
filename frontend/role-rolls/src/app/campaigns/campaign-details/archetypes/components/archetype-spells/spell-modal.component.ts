import { Component, signal } from '@angular/core';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { Spell } from '@app/models/spells/spell';
import { MarkdownViewerComponent } from '@app/shared/components/markdown-viewer/markdown-viewer.component';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormArray, FormBuilder, FormGroup } from '@angular/forms';
import { FloatLabel } from 'primeng/floatlabel';
import { InputText } from 'primeng/inputtext';
import { Textarea } from 'primeng/textarea';
import { ArchetypesService } from '@services/archetypes/archetypes.service';
import { canEditTemplate } from '@app/tokens/utils.funcs';
import { Campaign } from '@app/campaigns/models/campaign';
import { ButtonDirective } from 'primeng/button';

@Component({
  selector: 'rr-spell-modal',
  standalone: true,
  imports: [CommonModule, MarkdownViewerComponent, ReactiveFormsModule, FloatLabel, InputText, Textarea, ButtonDirective],
  template: `
  <div *ngIf="!editing()" (click)="tryEdit()" class="border rounded-md p-3" style="height: 70vh; overflow:auto">
    <rr-markdown-viewer [markdown]="spell.mdDescription"></rr-markdown-viewer>
  </div>

  <form *ngIf="editing()" [formGroup]="form" class="grid gap-3" style="max-height: 70vh; overflow:auto">
    <div class="grid grid-cols-2 gap-3">
      <p-floatlabel>
        <input pInputText id="name" formControlName="name" class="w-full" />
        <label for="name">Name</label>
      </p-floatlabel>
      <p-floatlabel>
        <textarea pTextarea id="description" formControlName="description" rows="2" class="w-full"></textarea>
        <label for="description">Description</label>
      </p-floatlabel>
    </div>

    <div formArrayName="circles" class="grid gap-4">
      <div *ngFor="let c of circles().controls; let i = index" [formGroupName]="i" class="border rounded-md p-3">
        <div class="grid grid-cols-2 gap-2">
          <p-floatlabel>
            <input pInputText id="title-{{i}}" formControlName="title" class="w-full" />
            <label for="title-{{i}}">Title</label>
          </p-floatlabel>
          <p-floatlabel>
            <input pInputText id="circle-{{i}}" formControlName="circle" class="w-full" />
            <label for="circle-{{i}}">Circle</label>
          </p-floatlabel>
          <p-floatlabel>
            <input pInputText id="level-{{i}}" formControlName="levelRequirement" class="w-full" />
            <label for="level-{{i}}">Level Requirement</label>
          </p-floatlabel>
          <p-floatlabel>
            <input pInputText id="casting-{{i}}" formControlName="castingTime" class="w-full" />
            <label for="casting-{{i}}">Casting Time</label>
          </p-floatlabel>
          <p-floatlabel>
            <input pInputText id="duration-{{i}}" formControlName="duration" class="w-full" />
            <label for="duration-{{i}}">Duration</label>
          </p-floatlabel>
          <p-floatlabel>
            <input pInputText id="area-{{i}}" formControlName="areaOfEffect" class="w-full" />
            <label for="area-{{i}}">Area of Effect</label>
          </p-floatlabel>
          <p-floatlabel class="col-span-2">
            <input pInputText id="req-{{i}}" formControlName="requirements" class="w-full" />
            <label for="req-{{i}}">Requirements</label>
          </p-floatlabel>
          <p-floatlabel class="col-span-2">
            <textarea pTextarea id="ingame-{{i}}" formControlName="inGameDescription" rows="2" class="w-full"></textarea>
            <label for="ingame-{{i}}">In-Game Description</label>
          </p-floatlabel>
          <p-floatlabel class="col-span-2">
            <textarea pTextarea id="effect-{{i}}" formControlName="effectDescription" rows="3" class="w-full"></textarea>
            <label for="effect-{{i}}">Effect Description</label>
          </p-floatlabel>
        </div>
      </div>
    </div>
    <div class="flex justify-end gap-2">
      <button pButton type="button" label="Save" icon="pi pi-check" (click)="save()"></button>
    </div>
  </form>
  `
})
export class SpellModalComponent {
  public spell!: Spell;
  public campaign!: Campaign;
  public templateId!: string;
  public archetypeId!: string;
  public editing = signal(false);
  public form!: FormGroup;

  constructor(config: DynamicDialogConfig, private ref: DynamicDialogRef, private fb: FormBuilder, private service: ArchetypesService) {
    this.spell = config.data.spell as Spell;
    this.campaign = config.data.campaign as Campaign;
    this.templateId = config.data.templateId as string;
    this.archetypeId = config.data.archetypeId as string;
    this.form = this.fb.group({ id: [''], name: [''], description: [''], circles: this.fb.array([]) });
    this.form.patchValue({ id: this.spell.id, name: this.spell.name, description: this.spell.description });
    const arr = this.circles();
    for (const c of (this.spell.circles ?? [])) {
      arr.push(this.fb.group({
        id: [c.id],
        circle: [c.circle],
        title: [c.title],
        inGameDescription: [c.inGameDescription],
        effectDescription: [c.effectDescription],
        castingTime: [c.castingTime],
        duration: [c.duration],
        areaOfEffect: [c.areaOfEffect],
        requirements: [c.requirements],
        levelRequirement: [c.levelRequirement],
      }));
    }
  }

  tryEdit() {
    if (canEditTemplate(this.campaign)) {
      this.editing.set(true);
    }
  }

  save() {
    if (!canEditTemplate(this.campaign)) return;
    const spell: Spell = { ...(this.form.value as any) } as Spell;
    this.service.updateSpell(this.templateId!, this.archetypeId!, spell).subscribe(() => {
      this.ref.close(spell);
    });
  }

  circles(): FormArray { return this.form.get('circles') as FormArray; }
}


