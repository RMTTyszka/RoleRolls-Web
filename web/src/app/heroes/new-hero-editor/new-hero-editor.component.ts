import {Component, OnInit} from '@angular/core';
import {NewHeroService} from '../new-hero.service';
import {DynamicDialogConfig, DynamicDialogRef, MessageService} from 'primeng/api';
import {ModalEntityAction} from '../../shared/dtos/ModalEntityData';
import {FormArray, FormGroup} from '@angular/forms';
import {DataService} from '../../shared/data.service';
import {NewHero} from '../../shared/models/NewHero.model';
import {take} from 'rxjs/operators';
import {Race} from '../../shared/models/Race.model';
import {Role} from '../../shared/models/Role.model';
import {Bonus} from '../../shared/models/Bonus.model';

@Component({
  selector: 'loh-new-hero-editor',
  templateUrl: './new-hero-editor.component.html',
  styleUrls: ['./new-hero-editor.component.css'],
  providers: [MessageService]
})
export class NewHeroEditorComponent  implements OnInit {
  public action: ModalEntityAction;
  public form = new FormGroup({});
  public attributes: string[] = [];
  public isLoading = true;
  public entity: NewHero;
  public entityId: string;
  public attributeDetailsIsOpened = false;
  private: MessageService
  constructor(
    public service: NewHeroService,
    private dataService: DataService,
    public ref: DynamicDialogRef, public config: DynamicDialogConfig,
    private messageService: MessageService
  ) {
    this.action = config.data.action;
    if (this.action === ModalEntityAction.update) {
      this.entityId = config.data.entity.id;
    }
    this.dataService.attributes2.subscribe(val => this.attributes = val);
  }

  ngOnInit() {
    this.service.onEntityChange.pipe(
      take(1)
    ).subscribe((entity: NewHero) => {
      console.log(entity);
      this.entity = entity;
    });
  }

  loaded(hasLoaded) {
    this.isLoading = !hasLoaded;
    this.form.get('level').valueChanges.subscribe(val => {
      this.dataService.getLevelData(val).subscribe(levelDetails => {
      //  this.form.get('nextLevel').setValue(levelDetails.expToNextLevel);
        this.form.get('totalAttributesBonusPoints').setValue(levelDetails.totalAttributesBonusPoints);
        this.form.get('maxAttributeBonusPoints').setValue(levelDetails.maxAttributesBonusPoints);
        this.form.get('totalAttributesInitialPoints').setValue(levelDetails.totalInitialAttributes);
        this.form.get('maxInitialAttributePoints').setValue(levelDetails.maxInitialAttributesPoints);
      });
    });
  }
  totalAttribute(attr: string) {
    return this.baseAttribute(attr) + this.bonusPointsAttribute(attr) + this.raceAttributeBonus(attr) + this.roleAttributeBonus(attr);
  }
  baseAttribute(attr: string) {
    return Number.parseInt(this.form.get('baseAttributes.' + attr).value, 10);
  }

  bonusPointsAttribute(attr: string) {
    return Number.parseInt(this.form.get('bonusAttributes.' + attr).value, 10);
  }
  raceAttributeBonus(attr: string): number {
    return (this.race.get('bonuses').value as Bonus[])
      .some(bonus => bonus.property === attr) ?
      (this.race.get('bonuses').value as Bonus[]).find(bonus => bonus.property === attr).level
      : 0;
  }
  roleAttributeBonus(attr: string): number {
    return (this.role.get('bonuses').value as Bonus[])
      .some(bonus => bonus.property === attr) ?
      (this.role.get('bonuses').value as Bonus[]).find(bonus => bonus.property === attr).level
      : 0;
  }
  addLevel() {
    this.form.get('level').setValue(this.level + 1);
  }
  removeLevel() {
    if (this.level > this.entity.level) {
      if (this.usedAttributesBonusPoints < this.totalAttributesBonusPoints && !this.hasMaximumUsedAttributesBonusPoints) {
        this.form.get('level').setValue(this.level - 1);
      } else {
        this.messageService.add({severity: 'error', detail: 'remove used bonus points', summary: 'Cannot reduce level'});
      }
    }
  }

  addAttr(attr: string) {
    if (this.usedAttributesBasePoints < this.totalAttributesInitialPoints
      && this.baseAttribute(attr) < this.maxInitialAttributesPoints) {
      this.form.get('baseAttributes.' + attr).setValue(this.baseAttribute(attr) + 1);
    } else if (this.usedAttributesBonusPoints < this.totalAttributesBonusPoints
      && this.bonusPointsAttribute(attr) < this.maxAttributeBonusPoints) {
      this.form.get('bonusAttributes.' + attr).setValue(this.bonusPointsAttribute(attr) + 1);
    }
  }

  removeAttr(attr: string) {
    if (this.bonusPointsAttribute(attr) > this.entity.level - 1) {
      this.form.get('bonusAttributes.' + attr).setValue(this.bonusPointsAttribute(attr) - 1);
    } else if (this.baseAttribute(attr) > 5 && this.action === ModalEntityAction.create) {
      this.form.get('baseAttributes.' + attr).setValue(this.baseAttribute(attr) - 1);
    }
  }
  resetAttrs() {
    if (this.action === ModalEntityAction.create) {
      this.attributes.forEach(attr => {
       this.form.get('baseAttributes.' + attr).setValue(8);
       this.form.get('bonusAttributes.' + attr).setValue(0);
      });
    } else if (this.action === ModalEntityAction.update) {
      this.attributes.forEach(attr => {
        this.form.get('bonusAttributes.' + attr).setValue(this.entity.bonusAttributes[attr]);
      });
    }
  }
  get usedAttributesBasePoints() {
    const total = this.attributes.reduce((p, n) => Number.parseInt(this.form.get('baseAttributes.' + n).value, 10) + p, 0);
    return total;
  }
  get usedAttributesBonusPoints() {
    const total = this.attributes.reduce((p, n) => Number.parseInt(this.form.get('bonusAttributes.' + n).value, 10) + p, 0);
    return total;
  }

  get hasMaximumUsedAttributesBonusPoints() {
    return this.attributes.some(attr => this.form.get('bonusAttributes.' + attr).value >= this.maxAttributeBonusPoints);
  }

  get totalAttributesInitialPoints(): number {
    return this.form.get('totalAttributesInitialPoints').value;
  }
  get maxInitialAttributesPoints(): number {
    return this.form.get('maxInitialAttributePoints').value;
  }
  get totalAttributesBonusPoints(): number {
    return this.form.get('totalAttributesBonusPoints').value;
  }
  get maxAttributeBonusPoints(): number {
    return this.form.get('maxAttributeBonusPoints').value;
  }
  get level(): number {
    return this.form.get('level').value;
  }
  raceSelected(race: Race) {
    console.log(this.form);
  }
  roleSelected(race: Role) {
    console.log(this.form);
  }
  get race() {
    return this.form.get('race') as FormGroup;
  }
  get role() {
    return this.form.get('role') as FormGroup;
  }
}
