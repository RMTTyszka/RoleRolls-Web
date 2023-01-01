import {BaseComponentConfig} from '../shared/components/base-component-config';
import {RRAction, RRColumns} from '../shared/components/rr-grid/r-r-grid.component';
import {DynamicDialogConfig} from 'primeng/dynamicdialog';
import {EncounterCreateEditComponent} from './encounter-create-edit/encounter-create-edit.component';
import { Encounter } from '../shared/models/Encounter.model';

export class EncounterConfig implements BaseComponentConfig<Encounter> {
  entityListActions: RRAction<Encounter>[] = [];
  navigateUrlOnRowSelect: string = null;
  navigateOnRowSelect = false;
  creatorOptions: DynamicDialogConfig = null;
  editorTitle = 'Race';
  editor = EncounterCreateEditComponent;
  creator = EncounterCreateEditComponent;
  entityListColumns: RRColumns[] = [
    {
      header: 'Encounter',
      property: 'name'
    }
  ];
  fieldName: string;
  selectModalColumns: RRColumns[] = [
    {
      header: 'Encounter',
      property: 'name'
    }
  ];
  selectModalTitle = 'Encounters';
  selectPlaceholder = 'Encounters';
  path = 'encounters';
}
