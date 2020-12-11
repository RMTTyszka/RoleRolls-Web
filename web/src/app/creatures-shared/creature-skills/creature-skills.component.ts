import {ChangeDetectionStrategy, ChangeDetectorRef, Component, Input, OnInit} from '@angular/core';
import {Creature} from '../../shared/models/creatures/Creature.model';
import {Skill} from '../../shared/models/skills/Skill';
import {DataService} from '../../shared/data.service';
import {FormGroup, FormGroupDirective} from '@angular/forms';
import {CreatureSkillsService} from './creature-skills.service';

@Component({
  selector: 'loh-creature-skills',
  templateUrl: './creature-skills.component.html',
  styleUrls: ['./creature-skills.component.css'],
  providers: [CreatureSkillsService]
})
export class CreatureSkillsComponent implements OnInit {


  @Input() creature: Creature;
  skillNames: string[] = [];
  form: FormGroup = new FormGroup({});
  get remainingPoints() {
    return this.form.get('remainingPoints').value;
  }
  get skills() {
    return this.form.value;
  }
  constructor(private dataService: DataService,
              private formDirective: FormGroupDirective,
              private dc: ChangeDetectorRef,
              private skillService: CreatureSkillsService) {
    this.dataService.getAllSkills().subscribe(skills => {
      this.skillNames = skills;
    });
  }

  ngOnInit(): void {
    this.form = this.formDirective.form.get('skills') as FormGroup;
  }

  getSkillPoint(skill: string) {
    return this.getSkill(skill).points || 0;
  }
  getMinorSkillPoint(skill: string, minorSkill: string) {
    return this.getSkill(skill)[minorSkill];
  }

  getSkill(skill: string): Skill {
    return this.form.get(skill).value;
  }
  async addMinorSkillPoint(skill: string, minorSkill: string) {
    const updatedSkills = await this.skillService.addMinorSkillPoint(this.creature.id, this.form.getRawValue(), minorSkill);
    this.form.patchValue(updatedSkills);
    this.dc.detectChanges();
  }
  async removeMinorSkillPoint(skill: string, minorSkill: string) {
    const updatedSkill = await this.skillService.removeMinorSkillPoint(this.creature.id, this.form.getRawValue(), minorSkill);
    this.form.patchValue(updatedSkill);
    this.dc.detectChanges();
  }
  async addSkillPoint(skillName: string) {
    const updatedSkill = await this.skillService.addMajorSkillPoint(this.creature.id, this.form.getRawValue(), skillName);
    this.form.patchValue(updatedSkill);
    this.dc.detectChanges();
  }
  async removeSkillPoint(skillName: string) {
    const updatedSkill = await this.skillService.removeMajorSkillPoint(this.creature.id, this.form.getRawValue(), skillName);
    this.form.patchValue(updatedSkill);
    this.dc.detectChanges();
  }

}
