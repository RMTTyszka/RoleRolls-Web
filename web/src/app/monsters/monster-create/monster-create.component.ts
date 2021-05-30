import { Component, OnInit } from '@angular/core';
import {FormGroup} from '@angular/forms';
import {MonsterService} from '../monster/monster.service';
import {Monster} from '../../shared/models/creatures/monsters/Monster.model';
import {createForm} from '../../shared/EditorExtension';
import {DialogService, DynamicDialogRef} from 'primeng/dynamicdialog';
import {MonsterModelsService} from '../monsters-bases/monster-template-provider/monster-models.service';
import {MonsterCreateInput} from '../../shared/models/creatures/monsters/MonsterCreateInput.model';
import {MonsterModel} from '../../shared/models/creatures/monsters/MonsterModel.model';

@Component({
  selector: 'rr-monster-create',
  templateUrl: './monster-create.component.html',
  styleUrls: ['./monster-create.component.css'],
  providers: [DialogService]
})
export class MonsterCreateComponent implements OnInit {

  form: FormGroup = new FormGroup({});
  private monster: MonsterCreateInput;
  public hasLoaded = false;
  constructor(
    private service: MonsterService,
    public monsterModelService: MonsterModelsService,
    private dialogRef: DynamicDialogRef
  ) {
    this.service.getNew().subscribe(newMonster => {
      this.monster = <MonsterCreateInput>newMonster;
      createForm(this.form, newMonster);
      console.log(this.form);
      this.hasLoaded = true;
    });
  }

  ngOnInit(): void {
  }

  save() {
    const monster = this.form.getRawValue();
    this.service.create(monster).subscribe(() => {
      this.dialogRef.close(monster);
    });
  }
}
