import {ItemTemplateModel} from '@app/models/ItemTemplateModel';
import {Entity} from '@app/models/Entity.model';

export interface ItemModel extends Entity {
  name: string;
  level: number;
  template: ItemTemplateModel;
}
