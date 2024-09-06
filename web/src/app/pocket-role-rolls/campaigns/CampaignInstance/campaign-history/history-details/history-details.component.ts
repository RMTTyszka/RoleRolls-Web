import {Component, Input, OnInit} from '@angular/core';
import {HistoryDto} from "../../../models/history-dto";
import {RollHistoryDto} from "../../../models/roll-history-dto";
import {ActionHistoryDto} from "../../../models/action-history-dto";
import {HistoryType} from "src/app/pocket-role-rolls/campaigns/models/history-type";

@Component({
  selector: 'rr-history-details',
  templateUrl: './history-details.component.html',
  styleUrls: ['./history-details.component.scss']
})
export class HistoryDetailsComponent implements OnInit {
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
