import { Injectable } from '@angular/core';
import {HttpClient, HttpParams} from '@angular/common/http';
import {LOH_API} from '../../loh.api';
import {PagedOutput} from '../../shared/dtos/PagedOutput';
import {CreatureSkills} from '../../shared/models/skills/CreatureSkills';
import {Skill} from '../../shared/models/skills/Skill';

@Injectable()
export class CreatureSkillsService {
  serverUrl = LOH_API.myBackUrl;
  path = 'skills/creature/'

  constructor(
    private http: HttpClient
  ) {
  }

  public addMajorSkillPoint(creatureId: string, creatureSkills: CreatureSkills, skillName: string): Promise<CreatureSkills> {
    creatureSkills.sports.list = null;
    creatureSkills.combat.list = null;
    creatureSkills.knowledge.list = null;
    creatureSkills.nimbleness.list = null;
    creatureSkills.perception.list = null;
    creatureSkills.relationship.list = null;
    creatureSkills.resistance.list = null;
    creatureSkills.skillsList = null;
    return this.http.post<CreatureSkills>(this.serverUrl + this.path + `${creatureId}` + '/addMajorSkillPoint', creatureSkills, {
      params: new HttpParams().set('skillName', skillName)
    }).toPromise();
  }
  public removeMajorSkillPoint(creatureId: string, creatureSkills: CreatureSkills, skillName: string): Promise<CreatureSkills> {
    creatureSkills.sports.list = null;
    creatureSkills.combat.list = null;
    creatureSkills.knowledge.list = null;
    creatureSkills.nimbleness.list = null;
    creatureSkills.perception.list = null;
    creatureSkills.relationship.list = null;
    creatureSkills.resistance.list = null;
    creatureSkills.skillsList = null;
    return this.http.post<CreatureSkills>(this.serverUrl + this.path + `${creatureId}` + '/removeMajorSkillPoint', creatureSkills, {
      params: new HttpParams().set('skillName', skillName)
    }).toPromise();
  }
  public addMinorSkillPoint(creatureId: string, creatureSkills: CreatureSkills, skillName: string): Promise<CreatureSkills> {
    creatureSkills.sports.list = null;
    creatureSkills.combat.list = null;
    creatureSkills.knowledge.list = null;
    creatureSkills.nimbleness.list = null;
    creatureSkills.perception.list = null;
    creatureSkills.relationship.list = null;
    creatureSkills.resistance.list = null;
    creatureSkills.skillsList = null;
    return this.http.post<CreatureSkills>(this.serverUrl + this.path + `${creatureId}` + '/addMinorSkillPoint', creatureSkills, {
      params: new HttpParams().set('skillName', skillName)
    }).toPromise();
  }
  public removeMinorSkillPoint(creatureId: string, creatureSkills: CreatureSkills, skillName: string): Promise<CreatureSkills> {
    creatureSkills.sports.list = null;
    creatureSkills.combat.list = null;
    creatureSkills.knowledge.list = null;
    creatureSkills.nimbleness.list = null;
    creatureSkills.perception.list = null;
    creatureSkills.relationship.list = null;
    creatureSkills.resistance.list = null;
    creatureSkills.skillsList = null;
    return this.http.post<CreatureSkills>(this.serverUrl + this.path + `${creatureId}` + '/removeMinorSkillPoint', creatureSkills, {
      params: new HttpParams().set('skillName', skillName)
    }).toPromise();
  }
}
