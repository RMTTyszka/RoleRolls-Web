import { Component, Input } from '@angular/core';
import { RollHistoryDto } from '@app/campaigns/models/roll-history-dto';
import { ActionHistoryDto } from '@app/campaigns/models/action-history-dto';
import { HistoryType } from '@app/campaigns/models/history-type';
import { HistoryDto } from '@app/campaigns/models/history-dto';
import { NgClass, NgIf, NgSwitch, NgSwitchCase } from '@angular/common';

@Component({
  selector: 'rr-log-details',
  imports: [
    NgSwitch,
    NgClass,
    NgSwitchCase,
    NgIf
  ],
  templateUrl: './log-details.component.html',
  styleUrl: './log-details.component.scss'
})
export class LogDetailsComponent {
  @Input() public history: HistoryDto;
  public rollHistory: RollHistoryDto;
  public actionHistory: ActionHistoryDto;
  public historyTypeEnum = HistoryType;

  constructor() {

  }

  ngOnInit(): void {
    this.defineType(this.history);
  }
  private defineType(history: HistoryDto) {
    switch (history.type) {
      case HistoryType.Action:
        this.actionHistory = this.history as ActionHistoryDto;
        break;
      case HistoryType.Roll:
        this.rollHistory = this.history as RollHistoryDto;
        break;
    }
  }
}
