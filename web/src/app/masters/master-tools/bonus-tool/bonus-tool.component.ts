import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {Bonus, BonusDurationEnum, BonusTypeEnum} from '../../../shared/models/Bonus.model';
import {Combat} from '../../../shared/models/combat/Combat.model';
import {Creature} from '../../../shared/models/creatures/Creature.model';
import {FormBuilder, FormGroup} from '@angular/forms';
import {SelectItem} from 'primeng/api';
import {DataService} from '../../../shared/data.service';
import {UpdateCreatureToolService} from '../update-creature-tool/update-creature-tool.service';
import {AddBonusInput} from '../../../shared/models/inputs/AddBonusInput';
@Component({
  selector: 'rr-bonus-tool',
  templateUrl: './bonus-tool.component.html',
  styleUrls: ['./bonus-tool.component.css']
})
export class BonusToolComponent implements OnInit {

  @Input() creature: Creature;
  @Input() combat: Combat;
  bonuses: Bonus[] = [];
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
      id: [],
      property: [],
      level: [],
      bonus: [],
      remainingTurns: [1],
      bonusType: [BonusTypeEnum.Innate],
      bonusDuration: [BonusDurationEnum.Unending]
    });
    this.bonusTypes = Object.values(BonusTypeEnum).map(b => <SelectItem> {value: b, label: b});
    this.bonusDurations = Object.values(BonusDurationEnum).map(b => <SelectItem> {value: b, label: b});
    this.dataService.getProperties().subscribe((properties) => {
      this.properties = properties.map(p => {
        return  {label: p, value: p} as SelectItem;
      });
    });

  }

  ngOnInit() {
    this.bonuses.push(...this.creature.bonuses);
    this.setNewBonus();
  }

  get newBonus() {
    return new Bonus('new');
  }
  get label(): string {
    return this.selectedBonus && this.selectedBonus.property === 'new' ? 'Add' : 'Update';
  }

  bonusSelected(bonus: Bonus) {
    this.form.patchValue(bonus);
    if (this.selectedBonus.property === 'new') {
      this.setNewBonus();
    } else {
      this.form.patchValue(bonus);
    }

  }
  save() {
    if (this.selectedBonus.property !== 'new') {
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
      }).subscribe((creature) => {
        this.bonuses = creature.bonuses;
        this.setNewBonus();
      });
    }
  }
  remove() {
    if (this.selectedBonus) {
      this.updateCreatureToolService.removeBonus(<AddBonusInput> {
        combatId: this.combat.id,
        creatureId: this.creature.id,
        bonus: this.form.value as Bonus
      }).subscribe(creature => {
        this.bonuses = creature.bonuses;
        this.setNewBonus();
      });
    }
  }

  get hasRemainingTurns() {
    return this.form.get('bonusDuration').value === BonusDurationEnum.ByTurn;
  }

  setNewBonus() {
    const bonus = this.newBonus;
    this.bonuses.push(bonus);
    this.selectedBonus = bonus;
    this.form.patchValue(bonus);
    this.form.get('property').setValue('Defense');
  }



}
