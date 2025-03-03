import {Entity} from '@app/models/Entity.model';
import { ItemTemplateModel } from '@app/models/itens/ItemTemplateModel';

export interface ItemModel extends Entity {
  name: string;
  level: number;
  template: ItemTemplateModel;
}
