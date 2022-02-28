import {Entity} from '../Entity.model';
import {ItemTemplateType} from './ItemTemplateType.enum';

export class ItemTemplate extends Entity {
  public description: string;
  public value: number;
  public itemTemplateType: ItemTemplateType;
}
