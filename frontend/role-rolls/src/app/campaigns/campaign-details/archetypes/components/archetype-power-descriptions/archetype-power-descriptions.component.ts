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
      const form = getAsForm(e)
      this.subscriptionManager.add(e.id, form.valueChanges.subscribe(v => {
        this.service.
      }))
      return form;
    }));
  });

  private subscriptionManager = new SubscriptionManager()
  constructor(
    private formBuilder: FormBuilder,
    private service: ArchetypesService,
  ) {

  }
  ngOnInit() {
    this.form = this.formBuilder.group({
      powers: this.powers()
    });
  }

  protected readonly FormGroup = FormGroup;
}
