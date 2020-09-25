import {Component, Inject, Injector, OnDestroy, OnInit} from '@angular/core';
import {LegacyBaseCreatorComponent} from 'src/app/shared/base-creator/legacy-base-creator.component';
import {HeroesService} from '../heroes.service';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {DataService} from 'src/app/shared/data.service';
import {RaceModalSelectorComponent} from 'src/app/races/shared/race-modal-selector/race-modal-selector.component';
import {RolesSelectModalComponent} from 'src/app/roles/roles-shared/roles-select-modal/roles-select-modal.component';
import {FormGroup} from '@angular/forms';
import {Race} from 'src/app/shared/models/Race.model';
import {Bonus} from 'src/app/shared/models/Bonus.model';
import {Hero} from '../../shared/models/NewHero.model';

@Component({
  selector: 'loh-heroes-editor',
  templateUrl: './heroes-editor.component.html',
  styleUrls: ['./heroes-editor.component.scss']
})
export class HeroesEditorComponent extends LegacyBaseCreatorComponent<Hero> implements OnInit {
  attributes: string[] = [];
  skills: string[] = [];
  totalInitialAttributePoints: number;
  totalAttributesBonusPoints: number;
  maximumAttributesBonusPoints: number;
  maxInitialAttributesPoints: number;
  constructor(
    injector: Injector,
    protected dialogRef: MatDialogRef<HeroesEditorComponent>,
    protected service: HeroesService,
    protected dataService: DataService,
    @Inject(MAT_DIALOG_DATA) data: any
  ) {
    super(injector, service);
    console.log(data);

    // this.service = injector.get(MonstersService);
     this.getEntity(data ? data.id : null);
     this.dataService.getAllSkills().subscribe(skills => this.skills = skills);
   }

  ngOnInit() {

  }
  ngOnDestroy() {
    super.ngOnDestroy();
  }


  afterGetEntity() {
    this.form.get('level').valueChanges.subscribe(val => {
      this.dataService.getLevelData(val).subscribe(levelDetails => {
          this.form.get('nextLevel').setValue(levelDetails.expToNextLevel);
          this.totalAttributesBonusPoints = levelDetails.totalAttributesBonusPoints;
          this.maximumAttributesBonusPoints = levelDetails.maxAttributesBonusPoints;
          this.totalInitialAttributePoints = levelDetails.totalInitialAttributes;
          this.maxInitialAttributesPoints = levelDetails.maxInitialAttributesPoints;
        });
    });
    this.form.get('level').disable();
    this.form.get('experience').disable();
    this.form.get('nextLevel').disable();
    this.dataService.getAllAttributes().subscribe(attrs => {
      this.attributes = attrs;
      if (this.action === 'create') {
      this.attributes.forEach(attr => {
        this.form.get('baseAttributes.' + attr).setValue(8);
      });
      }
    });
  }

  selectRace() {
    if (this.action === 'edit') {
      return;
    }
    this.dialog.open(RaceModalSelectorComponent).afterClosed().subscribe((race: Race) => {
      if (!race) {
        return;
      }
      console.log(race);
      const form = new FormGroup({});
      this.createForm(form, race);
      this.form.removeControl('race');
      this.form.addControl('race', form);
    });
  }
  selectRole() {
    if (this.action === 'edit') {
      return;
    }
    this.dialog.open(RolesSelectModalComponent).afterClosed().subscribe(role => {
      if (!role) {
        return;
      }
      const form = new FormGroup({});
      this.createForm(form, role);
      this.form.removeControl('role');
      this.form.addControl('role', form);
      role.bonuses.forEach(bonus => {
        let propertyCategory;
        if (this.attributes.indexOf(bonus.property) >= 0) {
          propertyCategory = 'attributes.';
        this.form.get(propertyCategory + bonus.property).setValue(this.form.get(propertyCategory + bonus.property).value + bonus.level);
        this.totalInitialAttributePoints += bonus.level;

        } else if (this.skills.indexOf(bonus.property) >= 0) {
          propertyCategory = 'skills.';
        this.form.get(propertyCategory + bonus.property).setValue(this.form.get(propertyCategory + bonus.property).value + bonus.bonus);
        }
      });
    });
  }

  get sumAttributesBonusPoints() {
    return this.attributes.map(attr => this.form.get('attributes.' + attr + 'BonusPoints').value).reduce((a, b) => a + b, 0);
  }
  get sumAttributesBasePoints() {
    return this.attributes.map(attr => this.form.get('baseAttributes.' + attr).value).reduce((a, b) => a + b, 0);
  }

  attributeAddButtonShouldBeDisabled(attr: string): boolean {

    if (!this.initialAttributesPointsCompleted) {
      return this.form.get('baseAttributes.' + attr).value >= this.maxInitialAttributesPoints;
    } else {
      return this.form.get('attributes.' + attr + 'BonusPoints').value >= this.maximumAttributesBonusPoints
      || this.sumAttributesBonusPoints >= this.totalAttributesBonusPoints;
    }

  }

  get initialAttributesPointsCompleted() {
    return this.sumAttributesBasePoints >= this.totalInitialAttributePoints;
  }
  attributeRemoveButtonShouldBeDisabled(attr: string): boolean {
    if (this.initialAttributesPointsCompleted) {
      return this.form.get('attributes.' + attr + 'BonusPoints').value <= 0;
    } else {
      return this.form.get('baseAttributes.' + attr).value <= 8;
    }
  }
  getTotalAttributeValue(attr: string) {
    const baseValue = this.form.get('baseAttributes.' + attr).value;
    const bonusPoints = this.form.get('attributes.' + attr + 'BonusPoints').value;
    const racePoints = this.getRaceAttrBalue(attr);
    return baseValue + bonusPoints + racePoints;
  }

  addAttributeBonusPoints(attr: string) {
    if (!this.attributeAddButtonShouldBeDisabled(attr)) {
      if (this.totalInitialAttributePoints > this.sumAttributesBasePoints) {
        this.form.get('baseAttributes.' + attr).setValue(this.form.get('baseAttributes.' + attr).value + 1);
      } else {
        this.form.get('attributes.' + attr + 'BonusPoints').setValue(this.form.get('attributes.' + attr + 'BonusPoints').value + 1);

      }
    }
  }
  removeAttributeBonusPoints(attr: string) {
    if (!this.attributeRemoveButtonShouldBeDisabled(attr)) {
      if (this.form.get('attributes.' + attr + 'BonusPoints').value > 0) {
      this.form.get('attributes.' + attr + 'BonusPoints').setValue(this.form.get('attributes.' + attr + 'BonusPoints').value - 1);

      } else if (this.form.get('baseAttributes.' + attr).value > 8) {
        this.form.get('baseAttributes.' + attr ).setValue(this.form.get('baseAttributes.' + attr).value - 1);
      }
    }
  }

  get sumSkillsBonusPoints() {
    return this.skills.map(sk => this.form.get('skills.' + sk + 'BonusPoints').value).reduce((a, b) => a + b, 0);
  }

  get totalSkillsBonusPoints() {
    return HeroesService.getTotalSkillsBonusPoints(this.form.get('level').value);
  }

  get maximumSkillsBonusPoints() {
    return HeroesService.getMaximumSkillsBonusPoints(this.form.get('level').value);
  }

  get levelUpDisabled(): boolean {
    return this.form.get('experience').value < this.form.get('nextLevel').value;
  }

  skillAddButtonShouldBeDisabled(sk: string): boolean {
    return this.form.get('skills.' + sk + 'BonusPoints').value >= this.maximumSkillsBonusPoints
     || this.sumSkillsBonusPoints >= this.totalSkillsBonusPoints;
  }
  skillRemoveButtonShouldBeDisabled(sk: string): boolean {
    return this.form.get('skills.' + sk + 'BonusPoints').value <= 0;
  }
  getTotalSkillValue(sk: string) {
    return this.form.get('skills.' + sk).value
    + this.form.get('skills.' + sk + 'BonusPoints').value;
  }

  addSkillBonusPoints(sk: string) {
    if (!this.skillAddButtonShouldBeDisabled(sk)) {

      this.form.get('skills.' + sk + 'BonusPoints').setValue(this.form.get('skills.' + sk + 'BonusPoints').value + 1);
    }
  }
  removeSkillBonusPoints(sk: string) {
    if (!this.skillRemoveButtonShouldBeDisabled(sk)) {
      this.form.get('skills.' + sk + 'BonusPoints').setValue(this.form.get('skills.' + sk + 'BonusPoints').value - 1);
    }
  }

  levelUp() {
    this.form.get('level').setValue(this.form.get('level').value + 1);
  }

  getRaceAttrBalue(attr: string) {
    return (this.form.get('race.bonuses').value as Bonus[])
    .filter(bonus => bonus.property === attr)
    .reduce((a, b ) => a + b.level, 0);
  }
  getRoleAttrBalue(attr: string) {
    return (this.form.get('role.bonuses').value as Bonus[])
    .filter(bonus => bonus.property === attr)
    .reduce((a, b ) => a + b.level, 0);
  }





}
