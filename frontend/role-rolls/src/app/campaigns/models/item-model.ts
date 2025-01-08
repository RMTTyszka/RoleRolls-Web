import {PocketEntity} from 'src/app/shared/models/pocket/pocket-entity';
import {ItemTemplateModel} from '../itens/ItemTemplateModel';

export class ItemModel extends PocketEntity {
  public name: string;
  public level: number;
  public template: ItemTemplateModel;
}
