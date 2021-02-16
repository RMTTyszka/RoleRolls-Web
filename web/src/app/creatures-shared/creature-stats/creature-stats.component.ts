import {Component, Input, OnDestroy, OnInit} from '@angular/core';
import {CreatureStatus} from '../../shared/models/creatures/CreatureStatus.model';
import {CreatureEditorService} from '../creature-editor/creature-editor.service';

@Component({
  selector: 'loh-creature-stats',
  templateUrl: './creature-stats.component.html',
  styleUrls: ['./creature-stats.component.css']
})
export class CreatureStatsComponent implements OnInit, OnDestroy {
  stats: string[] = ['defense', 'evasion', 'life', 'moral', 'dodge', 'specialAttack', 'magicDefense', 'mana', 'specialPower'];
  statsDescription = new Map<string, string>();
  @Input() status: CreatureStatus;
  constructor(
    private creatureEditorService: CreatureEditorService
  ) {
    this.creatureEditorService.subscribeToCreatureUpdated(CreatureStatsComponent.name, creature => this.status = creature.status);
    this.populateDescriptions();
  }



  ngOnInit() {
  }
  ngOnDestroy(): void {
    this.creatureEditorService.unsubscribeCreatureUpdated(CreatureStatsComponent.name);
  }

  getStatsDescription(stat: string) {
    return this.statsDescription.get(stat);
  }
  private populateDescriptions() {
    this.statsDescription.set('defense', 'Amount of damage reduction when receiving physical damage.');
    this.statsDescription.set('evasion', 'Bonus used agains an enemy attack');
    this.statsDescription.set('life', 'Amount of points reduced when receiving damage, after moral reaching zero');
    this.statsDescription.set('moral', 'Amount of points reduced when receiving damage, before losing life');
    this.statsDescription.set('dodge', 'Amount of attack automatically evaded');
    this.statsDescription.set('specialAttack', 'Bonus when attacking with special attacks');
    this.statsDescription.set('magicDefense', 'Amount of damage reduction when receiving magical or elemental damage.');
    this.statsDescription.set('mana', 'Points used by special powers');
    this.statsDescription.set('specialPower', 'Bonus when using special powers, usually not associated with an attack.');
  }
}
