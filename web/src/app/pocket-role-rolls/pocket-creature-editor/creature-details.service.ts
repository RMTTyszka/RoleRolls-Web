import { Injectable } from '@angular/core';
import {Subject} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CreatureDetailsService {

  public refreshCreature = new Subject<void>();
  public debug = new Subject<void>();
  constructor() { }
}
