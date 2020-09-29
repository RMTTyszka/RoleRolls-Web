import {Component, Injector, OnInit} from '@angular/core';
import {LegacyBaseListComponent} from 'src/app/shared/base-list/legacy-base-list.component';
import {HeroesEditorComponent} from '../heroes-editor/heroes-editor.component';
import {DataService} from 'src/app/shared/data.service';
import {Router} from '@angular/router';
import {DialogService} from 'primeng/dynamicdialog';
import {NewHeroEditorComponent} from '../new-hero-editor/new-hero-editor.component';
import {EditorAction} from '../../shared/dtos/ModalEntityData';
import {HeroesService} from '../heroes.service';
import {Hero} from '../../shared/models/NewHero.model';
import {HeroCreateComponent} from '../hero-create/hero-create.component';
import {HeroConfig} from '../hero-config';

@Component({
  selector: 'loh-heroes-list',
  templateUrl: './heroes-list.component.html',
  styleUrls: ['./heroes-list.component.css'],
  providers: [DialogService]
})
export class HeroesListComponent implements OnInit {
  config = new HeroConfig();
  constructor(
    protected service: HeroesService,
  ) {
   }
   ngOnInit() {
   }

}
