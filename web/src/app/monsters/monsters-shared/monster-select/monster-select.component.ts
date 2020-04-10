import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {map} from 'rxjs/operators';
import {MonsterService} from '../../monster/monster.service';
import {Monster} from '../../../shared/models/Monster.model';

@Component({
  selector: 'loh-monster-select',
  templateUrl: './monster-select.component.html',
  styleUrls: ['./monster-select.component.css']
})
export class MonsterSelectComponent implements OnInit {
  @Output() monsterSelected = new EventEmitter<Monster>();
  result: Monster[] = [];
  @Input() monster: Monster;
  constructor(
    private service: MonsterService,
  ) {
  }

  ngOnInit() {
  }

  search(event) {
    this.service.getAllFiltered(event).pipe(
      map(resp => resp.map(hero => hero))
    ).subscribe(response => this.result = response);
  }
  selected(monster: Monster) {
    this.monster = monster;
    this.monsterSelected.emit(monster);
  }

}
