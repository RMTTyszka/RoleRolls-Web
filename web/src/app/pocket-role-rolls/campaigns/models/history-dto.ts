import {HistoryType} from "./history-type";

export class HistoryDto {
  public asOfDate: string;
  public type: HistoryType;
  public actor: string;
}
