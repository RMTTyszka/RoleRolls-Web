import { Component, OnInit } from '@angular/core';
import { NewHeroService } from '../new-hero.service';
import { FormGroup } from '@angular/forms';
import { Race } from 'src/app/shared/models/Race.model';
import { Role } from 'src/app/shared/models/Role.model';
import { ModalEntityAction } from 'src/app/shared/dtos/ModalEntityData';

@Component({
  selector: 'loh-hero-creator',
  templateUrl: './hero-creator.component.html',
  styleUrls: ['./hero-creator.component.css']
})
export class HeroCreatorComponent implements OnInit {
  public action = ModalEntityAction.create;
  public form = new FormGroup({});
  public isLoading = true;
  constructor(
    public service: NewHeroService,
  ) { }

  ngOnInit() {
  }
  loaded(hasLoaded: boolean) {
    this.isLoading = !hasLoaded;
  }
  raceSelected(race: Race) {
    console.log(this.form);
  }
  roleSelected(race: Role) {
    console.log(this.form);
  }

}
