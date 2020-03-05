import { Component, OnInit, Injector } from '@angular/core';
import { BaseListComponent } from 'src/app/shared/base-list/base-list.component';
import { Hero } from 'src/app/shared/models/Hero.model';
import { HeroesEditorComponent } from '../heroes-editor/heroes-editor.component';
import { DataService } from 'src/app/shared/data.service';
import { Router } from '@angular/router';
import {DialogService} from 'primeng/api';
import {NewHeroEditorComponent} from '../new-hero-editor/new-hero-editor.component';
import {ModalEntityAction} from '../../shared/dtos/ModalEntityData';
import {NewHeroService} from '../new-hero.service';
import {NewHero} from '../../shared/models/NewHero.model';
import { HeroCreatorComponent } from '../hero-creator/hero-creator.component';

@Component({
  selector: 'loh-heroes-list',
  templateUrl: './heroes-list.component.html',
  styleUrls: ['./heroes-list.component.css'],
  providers: [DialogService]
})
export class HeroesListComponent extends BaseListComponent<NewHero> implements OnInit {

  constructor(
    injector: Injector,
    private dataService: DataService,
    protected service: NewHeroService,
    protected router: Router,
    private dialogService: DialogService
  ) {
    super(injector, service);
    this.editor = HeroesEditorComponent;
   }
   ngOnInit() {
    this.getAllFiltered();
    this.getAll = this.getAllFiltered;
   }

   addNewHero() {
    this.dialogService.open(HeroCreatorComponent, {
      header: 'Hero',
      width: '100vw',
      height: '100vh',
      data: {
        action: ModalEntityAction.create
      }
    }).onClose.subscribe();
   }
   updateHero(entity) {
    this.dialogService.open(NewHeroEditorComponent, {
      header: 'Hero',
      width: '100vw',
      height: '100vh',
      data: {
        entity: entity,
        action: ModalEntityAction.update
      }
    }).onClose.subscribe(() => {
      this.getAllFiltered();
    });
   }
}
