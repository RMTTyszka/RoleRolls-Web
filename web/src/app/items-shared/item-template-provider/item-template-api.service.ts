import {Injectable, Injector} from '@angular/core';
import {ItemTemplateProviderModule} from './item-template-provider.module';
import {BaseCrudService} from '../../shared/base-service/base-crud-service';
import {ItemTemplate} from '../../shared/models/items/ItemTemplate';
import {RRColumns} from '../../shared/components/rr-grid/r-r-grid.component';
import {ItemTemplateConfig} from '../item-template-config';

@Injectable({
  providedIn: ItemTemplateProviderModule
})
export class ItemTemplateApiService extends BaseCrudService<ItemTemplate, ItemTemplate> {
  entityListColumns: RRColumns[];
  fieldName: string;
  path: string;
  selectModalColumns: RRColumns[];
  selectModalTitle: string;
  selectPlaceholder: string;
  config = new ItemTemplateConfig();
  constructor(injector: Injector,
  ) {
    super(injector);
    BaseCrudService.setConfig(this, this.config);
  }
}
