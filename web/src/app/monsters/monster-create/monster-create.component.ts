import { Component, OnInit } from '@angular/core';
import {FormGroup} from '@angular/forms';
import {MonsterService} from '../monster/monster.service';
import {Monster} from '../../shared/models/Monster.model';
import {createForm} from '../../shared/EditorExtension';
import {DialogService} from 'primeng/dynamicdialog';
import {MonsterModelsService} from '../monsters-bases/monster-models.service';

@Component({
  selector: 'loh-monster-create',
  templateUrl: './monster-create.component.html',
  styleUrls: ['./monster-create.component.css'],
  providers: [DialogService]
})
export class MonsterCreateComponent implements OnInit {

  form: FormGroup = new FormGroup({});
  private monster: Monster;
  private hasLoaded = false;
  constructor(
    private service: MonsterService,
    public monsterModelService: MonsterModelsService,
    private dialogService: DialogService
  ) {
    this.service.getNew().subscribe(newMonster => {
      this.monster = newMonster;
      createForm(this.form, newMonster);
      this.hasLoaded = true;
    });
  }

  ngOnInit(): void {
  }

}
