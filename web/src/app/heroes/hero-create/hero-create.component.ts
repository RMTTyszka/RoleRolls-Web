import { Component, OnInit } from '@angular/core';
import {Race} from '../../shared/models/Race.model';
import {Role} from '../../shared/models/Role.model';
import {HeroesService} from '../heroes.service';
import {Hero} from '../../shared/models/NewHero.model';
import {EditorAction} from '../../shared/dtos/ModalEntityData';
import {FormGroup} from '@angular/forms';
import {DynamicDialogRef} from 'primeng/dynamicdialog';

@Component({
  selector: 'loh-hero-create',
  templateUrl: './hero-create.component.html',
  styleUrls: ['./hero-create.component.css']
})
export class HeroCreateComponent implements OnInit {
  public action = EditorAction.create;
  public form = new FormGroup({});
  public isLoading = true;
  public entity: Hero;
  public entityId: string;
  constructor(
    public service: HeroesService,
    public ref: DynamicDialogRef,
  ) { }

  ngOnInit(): void {
  }
  loaded(hasLoaded) {
    this.isLoading = !hasLoaded;
  }
  deleted() {
    this.ref.close(true);
  }

  raceSelected(race: Race) {
    console.log(this.form);
  }
  roleSelected(race: Role) {
    console.log(this.form);
  }

  saved() {
    this.ref.close(true);
  }
}
