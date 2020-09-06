import {Component, OnInit} from '@angular/core';
import {HeroesService} from '../heroes.service';
import {MessageService} from 'primeng/api';
import {EditorAction} from '../../shared/dtos/ModalEntityData';
import {FormGroup} from '@angular/forms';
import {DataService} from '../../shared/data.service';
import {Hero} from '../../shared/models/NewHero.model';
import {take} from 'rxjs/operators';
import {Race} from '../../shared/models/Race.model';
import {Role} from '../../shared/models/Role.model';
import {Bonus} from '../../shared/models/Bonus.model';
import {DynamicDialogConfig, DynamicDialogRef} from 'primeng/dynamicdialog';
import {HeroFundsService} from '../hero-funds/hero-funds.service';
import {HeroManagementService} from '../hero-management.service';

@Component({
  selector: 'loh-new-hero-editor',
  templateUrl: './new-hero-editor.component.html',
  styleUrls: ['./new-hero-editor.component.css'],
  providers: [MessageService, HeroFundsService]
})
export class NewHeroEditorComponent  implements OnInit {
  public action: EditorAction;
  public form = new FormGroup({});
  public attributes: string[] = [];
  public isLoading = true;
  public entity: Hero;
  public entityId: string;
  public attributeDetailsIsOpened = false;
  public get isCreating() {
    return this.action === EditorAction.create;
  }
  private: MessageService;
  constructor(
    public service: HeroesService,
    private dataService: DataService,
    public ref: DynamicDialogRef,
    public config: DynamicDialogConfig,
    private messageService: MessageService,
    private heroManagement: HeroManagementService
  ) {
    this.action = config.data.action;
    this.heroManagement.action = this.action;
    if (this.action === EditorAction.update) {
      this.entityId = config.data.entity.id;
    }
    this.dataService.attributes2.subscribe(val => this.attributes = val);
  }

  ngOnInit() {
    this.service.onEntityChange.pipe(
      take(1)
    ).subscribe((entity: Hero) => {
      console.log(entity);
      this.entity = entity;
      this.heroManagement.hero = entity;
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
    this.form.get('race').disable();
    this.form.get('role').disable();
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
    } else if (this.baseAttribute(attr) > 5 && this.action === EditorAction.create) {
      this.form.get('baseAttributes.' + attr).setValue(this.baseAttribute(attr) - 1);
    }
  }
  resetAttrs() {
    if (this.action === EditorAction.create) {
      this.attributes.forEach(attr => {
       this.form.get('baseAttributes.' + attr).setValue(8);
       this.form.get('bonusAttributes.' + attr).setValue(0);
      });
    } else if (this.action === EditorAction.update) {
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
  deleted() {
    this.ref.close(true);
  }

  attributeLevel(attr: string) {
    return Math.floor((this.totalAttribute(attr) + 4) / 5);
  }
  attributeBonusDice(attr: string) {
    return '1d' + (this.totalAttribute(attr) + 5) % 5 * 4;
  }
}
