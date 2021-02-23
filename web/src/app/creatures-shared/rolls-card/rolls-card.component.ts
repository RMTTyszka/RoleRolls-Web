import { Component, OnInit } from '@angular/core';
import {DynamicDialogConfig, DynamicDialogRef} from 'primeng/dynamicdialog';

export interface RollDifficulty {
  difficulty: number;
  complexity: number;
  requiredChance: number;
  shouldGetChance: boolean;
}

@Component({
  selector: 'loh-rolls-card',
  templateUrl: './rolls-card.component.html',
  styleUrls: ['./rolls-card.component.css']
})
export class RollsCardComponent implements OnInit {
  difficulty: number;
  complexity: number;
  shouldGetChance = false;
  chance: number;

  constructor(public ref: DynamicDialogRef, public config: DynamicDialogConfig) { }

  ngOnInit(): void {
  }

  roll() {
    this.ref.close(<RollDifficulty>{
      difficulty: this.difficulty || 10, complexity: this.complexity || 1
    });
  }
  getChance() {
    this.ref.close(<RollDifficulty>{
      difficulty: null, complexity: null, shouldGetChance: true, requiredChance: this.chance
    });
  }

  cancel() {
    this.ref.close();
  }
}
