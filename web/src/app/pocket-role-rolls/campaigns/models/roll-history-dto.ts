import {HistoryDto} from "./history-dto";

export class RollHistoryDto extends HistoryDto {
  public success: boolean;
  public rolls: string;
  public property: string;
  public bonus: number;
  public difficulty: number;
  public complexity: number;
}
