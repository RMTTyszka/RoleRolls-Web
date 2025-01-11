import { ItemTemplateModel } from '../../models/ItemTemplateModel';
import { Entity } from '../../models/Entity.model';

export interface ItemModel extends Entity {
  name: string;
  level: number;
  template: ItemTemplateModel;
}
