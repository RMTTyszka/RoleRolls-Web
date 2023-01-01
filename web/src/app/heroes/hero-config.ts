import {BaseComponentConfig} from '../shared/components/base-component-config';
import {NewHeroEditorComponent} from './new-hero-editor/new-hero-editor.component';
import {RRAction, RRColumns} from '../shared/components/rr-grid/r-r-grid.component';
import {HeroCreateComponent} from './hero-create/hero-create.component';
import {DynamicDialogConfig} from 'primeng/dynamicdialog';
import { Hero } from '../shared/models/NewHero.model';

export class HeroConfig implements BaseComponentConfig<Hero> {
  entityListActions: RRAction<Hero>[] = [];
  navigateUrlOnRowSelect: string = null;
  navigateOnRowSelect = false;
  editorTitle = 'Hero';
  editor = NewHeroEditorComponent;
  creator = HeroCreateComponent;
  creatorOptions = <DynamicDialogConfig>{width: '80%', height: '80%'};
  entityListColumns: RRColumns[] = [
    {
      header: 'Name',
      property: 'name'
    },
    {
      header: 'Race',
      property: 'race.name'
    },
    {
      header: 'Role',
      property: 'role.role'
    },
  ];
  fieldName: 'name';
  path: 'heroes';
  selectModalColumns: RRColumns[] = this.entityListColumns;
  selectModalTitle = 'Hero';
  selectPlaceholder: 'Hero';

}
