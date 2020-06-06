import {Injectable, Injector} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {LOH_API} from '../loh.api';
import {BaseCrudServiceComponent} from './base-service/base-crud-service.component';
import {Entity} from './models/Entity.model';
import {LevelDetails} from './dtos/LevelDetails';

@Injectable({
  providedIn: 'root'
})
export class DataService extends BaseCrudServiceComponent<Entity> {
  attributeUrl: string = LOH_API.attributeUrl;
  monstersUrl: string = LOH_API.monstersUrl;
  skillsMedievalUrl: string = LOH_API.skillsMedievalUrl;
  myBackUrl: string = LOH_API.myBackUrl;

  baseAttributes = 8;
  maxAttributes = 8 + 6 + 4 + 2 + 2;
  maxSkillValue = 2;
  maxPropertyValue = 6;

  attributes: string[];
  attributes2: Observable<string[]>;
  skills: string[];

  constructor(
    injector: Injector,
    protected http: HttpClient) {
    super(injector);
    this.attributes2 = this.getAllAttributes();
    this.getAllAttributes().subscribe(data => {
      this.attributes = data;
    });
    this.getAllSkills().subscribe(data => {
      this.skills = data.sort();
    });
   }

  getAllAttributes(): Observable<string[]> {
    return this.http.get<string[]>(this.myBackUrl + 'data/attributes');
  }
  getAllSkills(): Observable<string[]> {
    return this.http.get<string[]>(this.myBackUrl + 'data/skills');
  }

  getRollDicesInfo(lvl: number) {
    return this.getLevel(lvl) + '/ d' + this.getBonusDice(lvl);
  }
  getFullRollDicesInfo(lvl: number): string {
    return 'Level: ' + this.getLevel(lvl) + '/ bonus dice: 1d' + this.getBonusDice(lvl);
  }
  getLevel(level: number) {
    return Math.floor((level + 4) / 5);
  }
  getBonusDice(level: number) {
    return (level + 4) % 5 * 4;
  }
  getLevelData(level: number) {
    return this.http.get<LevelDetails>(this.myBackUrl + 'data/levelDetails/?level=' + level);
  }
  getProperties() {
    return this.http.get<Array<string>>(this.myBackUrl + 'data/properties');
  }



}
