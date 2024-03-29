import { Injectable } from '@angular/core';
import {BehaviorSubject} from 'rxjs';
import {Combat} from '../shared/models/combat/Combat.model';

@Injectable({
  providedIn: 'root'
})
export class CombatManagementService {
  public combatUpdated = new BehaviorSubject<Combat>(null);
  constructor() { }
}
