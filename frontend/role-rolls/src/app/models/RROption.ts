export interface RRAction<T> {
  icon: string;
  csClass: string | null | undefined;
  tooltip: string | null | undefined;
  callBack: (rowData: T, target: any) => void;
  condition: (rowData: T) => boolean;
}

export class RROption<T> {
  public label!: string;
  public value!: T;
  public csClass?: string | null | undefined;
}

