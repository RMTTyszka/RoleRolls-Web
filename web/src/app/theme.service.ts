import { Injectable } from '@angular/core';
import {Observable, Subject} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ThemeService {

  monsterTheme = new Subject<boolean>();
  playerTheme = new Subject<boolean>();
  mainTheme = new Subject<boolean>();
  constructor() {
    this.mainTheme.next(true);
  }

  setMainTheme() {
    this.mainTheme.next(true);
    this.playerTheme.next(false);
    this.monsterTheme.next(false);
  }
  setPlayerTheme() {
    this.mainTheme.next(false);
    this.playerTheme.next(true);
    this.monsterTheme.next(false);
  }
  setMonsterTheme() {
    this.mainTheme.next(false);
    this.playerTheme.next(false);
    this.monsterTheme.next(true);
  }
}
