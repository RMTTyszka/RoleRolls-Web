import {Component, Inject, OnInit} from '@angular/core';
import {AuthenticationService} from '../authentication/authentication.service';
import {SelectItem} from 'primeng';
import {UniverseType} from '../universes/universe';
import {UniverseService} from '../universes/universe.service';
import {SESSION_STORAGE, StorageService} from 'ngx-webstorage-service';

@Component({
  selector: 'loh-main-header',
  templateUrl: './main-header.component.html',
  styleUrls: ['./main-header.component.css']
})
export class MainHeaderComponent implements OnInit {
  title = 'Role Rolls';
  userName: string;
  universeOptions: SelectItem[] = [];
  selectedUniverse: UniverseType;

  get hasUser() {
    return this.authService.isLogged;
  }

  constructor(
    private readonly authService: AuthenticationService,
    private readonly universeService: UniverseService,
    @Inject(SESSION_STORAGE) private storage: StorageService
  ) {
    this.authService.userNameChanged.subscribe(userName => this.userName = userName);
    this.authService.getUser();
    this.createUniverseOptions();
  }

  ngOnInit(): void {
    this.selectedUniverse = this.storage.get(this.universeService.universeTypeCacheKey) || UniverseType.landOfHeroes;
    this.universeService.universe = this.selectedUniverse;
  }

  logout() {
    this.authService.cleanTokenAndUserName();
  }


  private createUniverseOptions() {
    this.universeOptions = [
      {value: UniverseType.landOfHeroes, label: 'Land Of Heroes'},
      {value: UniverseType.TheFutureIsOutThere, label: 'The Future is Out There'}
    ];
  }

  universeSelected(universe: UniverseType) {
    this.universeService.universe = universe;
  }
}

