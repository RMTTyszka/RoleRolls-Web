import { Component, OnInit, Injector, Inject } from '@angular/core';
import { BaseCreatorComponent } from '../../shared/base-creator/base-creator.component';
import { Monster } from '../../shared/models/Monster.model';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MonsterBaseSelectorComponent } from '../monsters-shared/monster-model-selector/monster-model-selector.component';
import { MonsterModel } from 'src/app/shared/models/MonsterModel.model';
import { MonsterService } from './monster.service';
import { Bonus } from 'src/app/shared/models/Bonus.model';

@Component({
  selector: 'loh-monster',
  templateUrl: './monster.component.html',
  styleUrls: ['./monster.component.css']
})
export class MonsterComponent extends BaseCreatorComponent<Monster> implements OnInit {
  attributes: string[];
  skills: string[];
  constructor(
    injector: Injector,
    protected dialogRef: MatDialogRef<MonsterComponent>,
    protected service: MonsterService,
    @Inject(MAT_DIALOG_DATA) data: any
  ) {
    super(injector, service);
   // this.service = injector.get(MonstersService);
    this.entity = data || new Monster();
  }
  ngOnInit() {
    this.attributes = this.dataService.attributes;
    this.skills = this.dataService.skills;

    this.getEntity(this.entity.id ? this.entity.id.toString() : null);
  }

  printMonster() {
    return Object.entries(this.form.value);
  }

  selectMonsterBase() {
    const dialogRef = this.dialog.open(MonsterBaseSelectorComponent);
    dialogRef.afterClosed().subscribe((monsterBase: MonsterModel) => {
      console.log(monsterBase);

      this.form.get('monsterBase').patchValue(monsterBase);
      this.skills.forEach(skill => {
        if (monsterBase.mainSkills.indexOf(skill) >= 0) {
          this.form.get('skills.' + skill).setValue(Math.floor(this.form.get('level').value * 0.5) + this.form.get('level').value + 2);
        } else {
          this.form.get('skills.' + skill).setValue(Math.floor(this.form.get('level').value * 0.5));
        }
      });
      this.attributes.forEach(attr => {
        this.form.get('attributes.' + attr).setValue(monsterBase.attributes[attr] + 8);
      });
      this.form.get('race').patchValue(monsterBase.race);
      monsterBase.race.bonuses.forEach((bonus: Bonus) => {
        let propertyCategory;
        if (this.attributes.indexOf(bonus.property) >= 0) {
          propertyCategory = 'attributes.';
        this.form.get(propertyCategory + bonus.property).setValue(this.form.get(propertyCategory + bonus.property).value + bonus.level);
        } else if (this.skills.indexOf(bonus.property) >= 0) {
          propertyCategory = 'skills.';
        this.form.get(propertyCategory + bonus.property).setValue(this.form.get(propertyCategory + bonus.property).value + bonus.bonus);
        }
      });
      this.form.get('role').patchValue(monsterBase.role);
      monsterBase.role.bonuses.forEach((bonus: Bonus) => {
        let propertyCategory;
        if (this.attributes.indexOf(bonus.property) >= 0) {
          propertyCategory = 'attributes.';
        this.form.get(propertyCategory + bonus.property).setValue(this.form.get(propertyCategory + bonus.property).value + bonus.level);
        } else if (this.skills.indexOf(bonus.property) >= 0) {
          propertyCategory = 'skills.';
        this.form.get(propertyCategory + bonus.property).setValue(this.form.get(propertyCategory + bonus.property).value + bonus.bonus);
        }
      });

      // this.form.get('monsterBase').setValue(monsterBase);
    });
  }
  selectRace() {
    console.log('not implemented');
  }
  selectRole() {
    console.log('not implemented');
  }

  get maxLife() { return 50 * 10; }

  afterGetEntity() {
    this.form.addControl('totalAttributeBonusPoints', this.fb.control(this.entity.level, ));
    this.attributes.forEach(attr => {
      if (this.action === 'create') {
        this.form.get('attributes.' + attr).setValue(8);
      }
    });
    this.form.get('level').valueChanges.subscribe(val => {
      this.form.get('totalAttributesBonusPoints').setValue((val - 1) * 2);
      this.form.get('maxAttributesBonusPoints').setValue(val - 1);

      this.skills.forEach(skill => {
        if (this.form.get('monsterBase').value.mainSkills.indexOf(skill) >= 0) {
          this.form.get('skills.' + skill).setValue(Math.floor(this.form.get('level').value * 1.5 + 2));
        } else {
          this.form.get('skills.' + skill).setValue(Math.floor(this.form.get('level').value * 0.5));
        }
      });
    });
  }

  getAttributeValueForView(attr: string) {
    return `${this.form.get('attributes.' + attr).value + this.form.get('attributes.' + attr + 'BonusPoints').value}
    - ${this.dataService.getFullRollDicesInfo(this.form.get('attributes.' + attr).value
      + this.form.get('attributes.' + attr + 'BonusPoints').value)}`;
  }

  attributeButtonIsDisabled(attr: string) {
    return this.form.get('attributes.' + attr + 'BonusPoints').value >= this.form.get('maxAttributesBonusPoints').value
     ||  this.totalUsedAttributesPoints >= this.form.get('totalAttributesBonusPoints').value;
  }

  get totalUsedAttributesPoints() {
    return this.attributes.map(attr => this.form.get('attributes.' + attr + 'BonusPoints').value).reduce((a, b) => a + b, 0);
  }
  addAttributePoint(attr: string) {
    if (this.totalUsedAttributesPoints <= this.form.get('totalAttributesBonusPoints').value
     && this.form.get('attributes.' + attr + 'BonusPoints').value <=  this.form.get('maxAttributesBonusPoints').value) {
       this.form.get('attributes.' + attr + 'BonusPoints').setValue(this.form.get('attributes.' + attr + 'BonusPoints').value + 1);
     }
  }
  removeAttributePoint(attr: string) {
    if (this.form.get('attributes.' + attr + 'BonusPoints').value > 0) {
      this.form.get('attributes.' + attr + 'BonusPoints').setValue(this.form.get('attributes.' + attr + 'BonusPoints').value - 1);
    }
  }

  get totalUsedSkillsPoints() {
    return this.skills.map(sk => this.form.get('skills.' + sk + 'BonusPoints').value).reduce((a, b) => a + b, 0);
  }
  addSkillsPoint(sk: string) {
    if (this.totalUsedSkillsPoints <= this.form.get('totalSkillsBonusPoints').value
     && this.form.get('skills.' + sk + 'BonusPoints').value <=  this.form.get('maxSkillsBonusPoints').value) {
       this.form.get('skills.' + sk + 'BonusPoints').setValue(this.form.get('skills.' + sk + 'BonusPoints').value + 1);
     }
  }
  removeSkillsPoint(sk: string) {
    if (this.form.get('skills.' + sk + 'BonusPoints').value > 0) {
      this.form.get('skills.' + sk + 'BonusPoints').setValue(this.form.get('skills.' + sk + 'BonusPoints').value - 1);
    }
  }
  skillButtonIsDisabled(attr: string) {
    return this.form.get('skills.' + attr + 'BonusPoints').value >= this.form.get('maxSkillsBonusPoints').value
     ||  this.totalUsedSkillsPoints >= this.form.get('totalSkillsBonusPoints').value;
  }

}
