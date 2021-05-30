import {Component, Inject, Injector, OnDestroy, OnInit} from '@angular/core';
import {FormArray, FormControl} from '@angular/forms';
import {Subscription} from 'rxjs';
import {MonsterModel} from 'src/app/shared/models/creatures/monsters/MonsterModel.model';
import {Race} from 'src/app/shared/models/Race.model';
import {Role} from 'src/app/shared/models/Role.model';
import {RolesService} from '../../../roles/roles.service';
import {RacesService} from '../../../races/races.service';
import {BaseCreatorComponent} from '../../../shared/base-creator/base-creator.component';
import {DynamicDialogConfig, DynamicDialogRef} from 'primeng/dynamicdialog';
import {MonsterModelsService} from '../monster-template-provider/monster-models.service';
import {DataService} from '../../../shared/data.service';

@Component({
  selector: 'rr-monster-model',
  templateUrl: './monster-model.component.html',
  styleUrls: ['./monster-model.component.scss']
})
export class MonsterModelComponent extends BaseCreatorComponent<MonsterModel, MonsterModel> implements OnInit, OnDestroy  {
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

  get race() {
    return this.entity ? this.entity.race : this.createEntity.race;
  }
  get role() {
    return this.entity ? this.entity.role : this.createEntity.role;
  }
  constructor(
    injector: Injector,
    public dialogRef: DynamicDialogRef,
    public dialogConfig: DynamicDialogConfig,
    public service: MonsterModelsService,
    protected rolesService: RolesService,
    private dataService: DataService,
    protected racesService: RacesService,
  ) {
    super(injector);
    if (dialogConfig.data) {
      this.getEntity(dialogConfig.data.entityId);
    } else {
      this.getEntity();
    }
  }

  ngOnInit() {
  }

  ngOnDestroy() {
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
    this.totalPointsSkills = this.entity ? this.entity.role.skillPoints : 6;
    this.isLoading = false;
  }
  raceSelected(race: Race) {
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
  }
  roleSelected(role: Role) {
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
      const raceBon = this.race.bonuses.find(
        b => b.property === attr
      );
      const roleBon = this.role.bonuses.find(
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
      this.form.get('attributes.' + attr).setValue(this.entity ? this.entity.attributes[attr] : 0);
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
    this.form.addControl('mainSkills', this.fb.array(this.entity ? this.entity.mainSkills.map(sk => new FormControl(sk, [])) : []));
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
