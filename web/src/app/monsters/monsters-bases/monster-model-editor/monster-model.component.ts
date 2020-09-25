import {Component, Inject, Injector, OnDestroy, OnInit} from '@angular/core';
import {FormArray, FormControl} from '@angular/forms';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {Subscription} from 'rxjs';
import {RaceModalSelectorComponent} from 'src/app/races/shared/race-modal-selector/race-modal-selector.component';
import {RolesSelectModalComponent} from 'src/app/roles/roles-shared/roles-select-modal/roles-select-modal.component';
import {LegacyBaseCreatorComponent} from 'src/app/shared/base-creator/legacy-base-creator.component';
import {MonsterModel} from 'src/app/shared/models/MonsterModel.model';
import {Race} from 'src/app/shared/models/Race.model';
import {Role} from 'src/app/shared/models/Role.model';
import {MonsterBaseService} from './monster-model.service';

@Component({
  selector: 'loh-monster-model',
  templateUrl: './monster-model.component.html',
  styleUrls: ['./monster-model.component.css']
})
export class MonsterModelComponent extends LegacyBaseCreatorComponent<MonsterModel>
  implements OnInit, OnDestroy  {
  totalPointsAttributes: number;
  totalPointsSkills: number;
  maxSKillValue: number;
  attributes: string[] = [];
  skills: string[] = [];

  attributesIsLoading = true;
  skillsIsLoading = true;
  attributesSubscription: Subscription;
  skillsSubscription: Subscription;
  maxPropertyValue;
  displayedColumns = [
    'name',
    'base',
    'race',
    'role',
    'points',
    'actions ',
    'total'
  ];
  constructor(
    injector: Injector,
    public dialogRef: MatDialogRef<MonsterModelComponent>,
    protected service: MonsterBaseService,
    @Inject(MAT_DIALOG_DATA) data: any
  ) {
    super(injector, service);
    this.getEntity(data ? data.id : null);
  }

  ngOnInit() {
  }

  ngOnDestroy() {
    super.ngOnDestroy();
  }
  afterGetEntity() {
    this.addDataToForm();

    this.getAttributes();

    this.getSkills();
  }

  addDataToForm() {
    this.maxPropertyValue = this.dataService.maxPropertyValue;
    this.maxSKillValue = this.dataService.maxSkillValue;
    this.totalPointsAttributes = this.dataService.maxAttributes;
    this.totalPointsSkills = this.entity.role.skillPoints || 3;
    this.isLoading = false;
  }
  selectRace() {
    const raceDialog = this.dialog.open(RaceModalSelectorComponent);
    raceDialog.afterClosed().subscribe((race: Race) => {
      if (!race) {
        return;
      }
      if (this.form.get('race').value) {
        this.form.get('race.bonuses').value.forEach(bon => {
          this.form.get(bon.property + 'Race').setValue(0);
        });
      }
      this.form.get('race').patchValue(race);
      race.bonuses.forEach(bon => {
        this.form.get(bon.property + 'Race').setValue(bon.level);
        this.calculateTotalProperty(bon.property);
      });
    });
  }
  selectRole() {
    const raceDialog = this.dialog.open(RolesSelectModalComponent);
    raceDialog.afterClosed().subscribe((role: Role) => {
      if (!role) {
        return;
      }
      if (this.form.get('role').value) {
        this.form.get('role.bonuses').value.forEach(bon => {
          this.form.get(bon.property + 'Role').setValue(0);
        });
      }
      this.form.get('role').patchValue(role);
      role.bonuses.forEach(bon => {
        this.form.get(bon.property + 'Role').setValue(bon.level);
        this.calculateTotalProperty(bon.property);
      });
    });
  }

  calculateTotalProperty(attr: string) {
    this.form
      .get(attr + 'Total')
      .setValue(
        parseInt(this.form.get(attr + 'Base').value, 10) +
          parseInt(this.form.get(attr + 'Race').value, 10) +
          parseInt(this.form.get(attr + 'Role').value, 10) +
          parseInt(this.form.get('attributes.' + attr).value || 0, 10) +
          ((<FormArray>this.form.get('bonuses')).controls.find(
            b => b.value.property === attr
          )
            ? parseInt(
                (<FormArray>this.form.get('bonuses')).controls.find(
                  b => b.value.property === attr
                ).value.level,
                10
              )
            : 0)
      );
  }
  increaseProperty(property: string) {
    this.form.get('attributes.' + property).setValue(this.form.get('attributes.' + property).value + 1);
  }
  decreaseProperty(property: string) {
    this.form.get('attributes.' + property).setValue(this.form.get('attributes.' + property).value - 1);
  }
  update() {
    this.attributes.forEach(a => this.calculateTotalProperty(a));
  }

  getTotalAttributes() {
    return this.attributes.reduce((a, b) => a + this.form.get('attributes.' + b).value, 0);
  }
  getAttributes() {
    if (this.dataService.attributes) {
      this.attributes = this.dataService.attributes;
      this.setAttributesForm();
    } else {
      this.dataService.getAllAttributes().subscribe(attributes => {
        this.attributes = attributes;
        this.setAttributesForm();
      });
    }
    }
  setAttributesForm() {
        this.attributes.forEach(attr => {
          const raceBon = this.entity.race.bonuses.find(
            b => b.property === attr
          );
          const roleBon = this.entity.role.bonuses.find(
            b => b.property === attr
          );

          this.form.addControl(
            attr + 'Base',
            this.fb.control(this.dataService.baseAttributes, [])
          );

          this.form.addControl(
            attr + 'Race',
            this.fb.control(raceBon ? raceBon.level : 0, [])
          );
          this.form.addControl(
            attr + 'Role',
            this.fb.control(roleBon ? roleBon.level : 0, [])
          );
          this.form.addControl(attr + 'Total', this.fb.control(0, []));

          this.calculateTotalProperty(attr);
          this.form.get('attributes.' + attr).valueChanges.subscribe(val => {
            if (val > this.maxPropertyValue) {
              this.form.get('attributes.' + attr).setValue(this.maxPropertyValue);
            }

            this.calculateTotalProperty(attr);
          });
          this.form.get('attributes.' + attr).setValue(this.entity.attributes[attr] || 0);
        });

        this.attributesIsLoading = false;
    }
    getSkills() {

      if (this.dataService.skills) {
        this.skills = this.dataService.skills;
        this.setSkillsForm();
      } else {
        this.dataService.getAllSkills().subscribe(skills => {
          this.skills = skills;
          this.setSkillsForm();
        });
      }
    }
  setSkillsForm() {
        this.form.addControl('mainSkills', this.fb.array(this.entity.mainSkills.map(sk => new FormControl(sk, []))));
        this.skillsIsLoading = false;
        console.log(this.form.value);

    }
  addSkill(skill: string) {
    if (this.selectedSkills.controls.length >= this.totalPointsSkills && !this.skillIsSelected(skill)) {
      return;
    }
    if (this.selectedSkills.controls.some(c => c.value === skill)) {
      this.removeSkill(skill);
    } else {
      this.selectedSkills.push(new FormControl(skill, []));
    }
    console.log(this.form.controls.mainSkills.value);

  }
  removeSkill(skill: string) {
    this.selectedSkills.removeAt(this.selectedSkills.controls.findIndex(control => control.value === skill));
  }
  resetSkills() {
    this.selectedSkills.setValue([]);
  }
  skillIsSelected(skill: string) {
    return this.selectedSkills.controls.some(c => c.value === skill);
  }

  resetAttrinutes() {
   this.attributes.forEach(attr => {
     this.form.get(attr).setValue(0);
   });
  }
  get selectedSkills() {
    return this.form.get('mainSkills') as FormArray;
  }
}
