import { Component, computed, input, signal } from '@angular/core';
import { Archetype } from '@app/models/archetypes/archetype';
import { CommonModule } from '@angular/common';
import { DialogService } from 'primeng/dynamicdialog';
import { ArchetypesService } from '@services/archetypes/archetypes.service';
import { ButtonDirective } from 'primeng/button';
import { Spell } from '@app/models/spells/spell';
import { SpellModalComponent } from './spell-modal.component';
import { Campaign } from '@app/campaigns/models/campaign';

@Component({
  selector: 'rr-archetype-spells',
  standalone: true,
  imports: [CommonModule, ButtonDirective],
  templateUrl: './archetype-spells.component.html'
})
export class ArchetypeSpellsComponent {
  public archetype = input<Archetype>();
  public campaign = input<Campaign>();

  private spellsOverride = signal<Spell[] | null>(null);
  public spells = computed<Spell[]>(() => {
    const list = this.spellsOverride() ?? (this.archetype()?.spells ?? []);
    return [...list].sort((a, b) => a.name.localeCompare(b.name, 'pt-BR'));
  });

  constructor(private dialog: DialogService, private archetypes: ArchetypesService) {}

  open(spell: Spell) {
    const ref = this.dialog.open(SpellModalComponent, { header: spell.name, width: '70vw', data: { spell, campaign: this.campaign(), archetypeId: this.archetype()?.id, templateId: this.campaign()?.campaignTemplateId } });
    ref.onClose.subscribe(result => {
      if (result && this.campaign()?.campaignTemplateId && this.archetype()?.id) {
        this.archetypes.getById(this.campaign()!.campaignTemplateId!, this.archetype()!.id!)
          .subscribe(a => this.spellsOverride.set(a.spells ?? []));
      }
    });
  }
}



