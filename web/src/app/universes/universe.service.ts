import {Inject, Injectable} from '@angular/core';
import {UniverseType} from './universe';
import {SESSION_STORAGE, StorageService} from 'ngx-webstorage-service';
import {Subject} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UniverseService {
  public universeTypeCacheKey = 'universeTypeCacheKey';
  public universeChanged = new Subject<boolean>();
  private _universe: UniverseType;

  public get universe() {
    return this._universe;
  }
  public set universe(universe: UniverseType) {
    this._universe = universe;
    this.universeChanged.next(true);
  }
  constructor(
    @Inject(SESSION_STORAGE) private storage: StorageService
  ) { }

}
