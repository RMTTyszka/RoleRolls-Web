import {Component, Injector, OnInit} from '@angular/core';
import {BaseListComponent} from 'src/app/shared/base-list/base-list.component';
import {HeroesEditorComponent} from '../heroes-editor/heroes-editor.component';
import {DataService} from 'src/app/shared/data.service';
import {Router} from '@angular/router';
import {DialogService} from 'primeng/dynamicdialog';
import {NewHeroEditorComponent} from '../new-hero-editor/new-hero-editor.component';
import {ModalEntityAction} from '../../shared/dtos/ModalEntityData';
import {NewHeroService} from '../new-hero.service';
import {Hero} from '../../shared/models/NewHero.model';

@Component({
  selector: 'loh-heroes-list',
  templateUrl: './heroes-list.component.html',
  styleUrls: ['./heroes-list.component.css'],
  providers: [DialogService]
})
export class HeroesListComponent extends BaseListComponent<Hero> implements OnInit {

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
    this.dialogService.open(NewHeroEditorComponent, {
      header: 'Hero',
      width: '100vw',
      height: '100vh',
      data: {
        action: ModalEntityAction.create
      }
    }).onClose.subscribe((createdHero: Hero) => {
      if (createdHero) {
        this.service.getEntity(createdHero.id).subscribe((hero: Hero) => {
          this.updateHero(hero);
        })
      }
    });
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
