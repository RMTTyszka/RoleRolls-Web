import {Component, Input, OnInit} from '@angular/core';
import { HistoryDto } from '../../models/history-dto';
import {HistoryType} from "../../models/history-type";
import {RollHistoryDto} from "../../models/roll-history-dto";

@Component({
  selector: 'rr-history-details',
  templateUrl: './history-details.component.html',
  styleUrls: ['./history-details.component.scss']
})
export class HistoryDetailsComponent implements OnInit {
  @Input() public history: HistoryDto;
  public rollHistory: RollHistoryDto;
  public historyTypeEnum = HistoryType;

  constructor() {

  }

  ngOnInit(): void {
    this.defineType(this.history);
  }
  private defineType(history: HistoryDto) {
    switch (history.type) {
      case HistoryType.Roll:
        this.rollHistory = this.history as RollHistoryDto;
    }
  }
}
