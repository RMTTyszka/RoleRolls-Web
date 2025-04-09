import { Component, computed, input } from '@angular/core';
import { Archetype, ArchetypePowerDescription } from '@app/models/archetypes/archetype';
import { Fieldset } from 'primeng/fieldset';
import { AbstractControl, FormArray, FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { getAsForm } from '@app/tokens/EditorExtension';
import { JsonPipe } from '@angular/common';
import { FloatLabel } from 'primeng/floatlabel';
import { Textarea } from 'primeng/textarea';

@Component({
  selector: 'rr-archetype-power-descriptions',
  imports: [
    Fieldset,
    JsonPipe,
    ReactiveFormsModule,
    FloatLabel,
    Textarea
  ],
  templateUrl: './archetype-power-descriptions.component.html',
  styleUrl: './archetype-power-descriptions.component.scss'
})
export class ArchetypePowerDescriptionsComponent {
  public form: FormGroup;
  public archetype = input<Archetype>();
  public powers = computed<FormArray>(() => {
    return this.formBuilder.array(this.archetype().powerDescriptions.map(e => {
      return getAsForm(e);
    }));
  });

  constructor(
    private formBuilder: FormBuilder,
  ) {

  }
  ngOnInit() {
    this.form = this.formBuilder.group({
      powers: this.powers()
    });
  }

  protected readonly FormGroup = FormGroup;
}
