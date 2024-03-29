import {Component, Injector, OnInit} from '@angular/core';
import {DialogService} from 'primeng/dynamicdialog';
import {HeroesService} from '../heroes.service';
import {HeroConfig} from '../hero-config';

@Component({
  selector: 'rr-heroes-list',
  templateUrl: './heroes-list.component.html',
  styleUrls: ['./heroes-list.component.css'],
  providers: [DialogService]
})
export class HeroesListComponent implements OnInit {
  config = new HeroConfig();
  constructor(
    public service: HeroesService,
  ) {
   }
   ngOnInit() {
   }

}
