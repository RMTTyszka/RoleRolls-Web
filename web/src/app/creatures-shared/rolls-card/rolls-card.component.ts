import { Component, OnInit } from '@angular/core';
import {DynamicDialogConfig, DynamicDialogRef} from 'primeng/dynamicdialog';

export interface RollDifficulty {
  difficulty: number;
  complexity: number;
}

@Component({
  selector: 'loh-rolls-card',
  templateUrl: './rolls-card.component.html',
  styleUrls: ['./rolls-card.component.css']
})
export class RollsCardComponent implements OnInit {
  difficulty: number;
  complexity: number;

  constructor(public ref: DynamicDialogRef, public config: DynamicDialogConfig) { }

  ngOnInit(): void {
  }

  roll() {
    this.ref.close(<RollDifficulty>{
      difficulty: this.difficulty || 10, complexity: this.complexity || 1
    });
  }

  cancel() {
    this.ref.close();
  }
}
