import {Entity} from './Entity.model';
import {ItemTemplateType} from './ItemTemplateType.enum';

export class ItemTemplate extends Entity {

  public level = 1;
  public description = '';
  public bonus = 0;
  public name = '';
  public itemTemplateType: ItemTemplateType;
}
