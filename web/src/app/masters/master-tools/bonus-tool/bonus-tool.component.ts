import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {Bonus, BonusDurationEnum, BonusTypeEnum} from '../../../shared/models/Bonus.model';
import {Combat} from '../../../shared/models/combat/Combat.model';
import {Creature} from '../../../shared/models/creatures/Creature.model';
import {FormBuilder, FormGroup} from '@angular/forms';
import {SelectItem} from 'primeng/api';
import {DataService} from '../../../shared/data.service';
import {UpdateCreatureToolService} from '../update-creature-tool/update-creature-tool.service';
import {AddBonusInput} from '../../../shared/models/inputs/AddBonusInput';
import {UUID} from 'angular2-uuid';

@Component({
  selector: 'loh-bonus-tool',
  templateUrl: './bonus-tool.component.html',
  styleUrls: ['./bonus-tool.component.css']
})
export class BonusToolComponent implements OnInit {

  @Input() creature: Creature;
  @Input() combat: Combat;
  form: FormGroup;
  selectedBonus: Bonus;
  properties: SelectItem[];
  bonusTypes: SelectItem[];
  bonusDurations: SelectItem[];
  constructor(
    private fb: FormBuilder,
    private dataService: DataService,
  private readonly updateCreatureToolService: UpdateCreatureToolService,
  ) {
    this.form = this.fb.group({
      id: [UUID.UUID()],
      property: [],
      level: [],
      bonus: [],
      remainingTurns: [1],
      bonusType: [BonusTypeEnum.Innate],
      bonusDuration: [BonusDurationEnum.Unending]
    });
    this.bonusTypes = Object.values(BonusTypeEnum).map(b => <SelectItem> {value: b, label: b})
    this.bonusTypes = Object.values(BonusDurationEnum).map(b => <SelectItem> {value: b, label: b})
    this.dataService.getProperties().subscribe((properties) => {
      this.properties = properties.map(p => {
        return  {label: p, value: p} as SelectItem;
      });
    });
  }

  ngOnInit() {
  }

  get bonuses(): Array<Bonus> {
    return this.creature.bonuses.filter(b => b.bonusType !== 'Innate');
  }
  get label(): string {
    return this.selectedBonus ? 'Update' : 'Add';
  }

  bonusSelected(bonus: Bonus) {
    console.log(bonus)
    this.form.patchValue(bonus);

  }
  save() {
    if (this.selectedBonus) {
      this.updateCreatureToolService.updateBonus(<AddBonusInput> {
        combatId: this.combat.id,
        creatureId: this.creature.id,
        bonus: this.form.value as Bonus
      }).subscribe();
    } else {
      this.updateCreatureToolService.addBonus(<AddBonusInput> {
        combatId: this.combat.id,
        creatureId: this.creature.id,
        bonus: this.form.value as Bonus
      }).subscribe();
    }
  }
  remove() {
    if (this.selectedBonus) {
      this.updateCreatureToolService.removeBonus(<AddBonusInput> {
        combatId: this.combat.id,
        creatureId: this.creature.id,
        bonus: this.form.value as Bonus
      }).subscribe();
    } else {
      this.updateCreatureToolService.addBonus(<AddBonusInput> {
        combatId: this.combat.id,
        creatureId: this.creature.id,
        bonus: this.form.value as Bonus
      }).subscribe();
    }
  }

  get hasRemainingTurns() {
    return this.form.get('bonusDuration').value === BonusDurationEnum.ByTurn;
  }




}
