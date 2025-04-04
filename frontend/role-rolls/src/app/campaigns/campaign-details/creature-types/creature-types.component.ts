import {Component, EventEmitter, signal} from '@angular/core';
import {GridComponent, RRColumns, RRHeaderAction} from "@app/components/grid/grid.component";
import {CreatureType} from '@app/models/creatureTypes/creature-type';
import {ActivatedRoute, Router} from '@angular/router';
import {CreatureTypesService} from '@services/creature-types/creature-types.service';
import {Campaign} from '@app/campaigns/models/campaign';
import {AuthenticationService} from '@app/authentication/services/authentication.service';
import { DialogService, DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import {
  CreatureTypeEditorComponent
} from '@app/campaigns/campaign-details/creature-types/creature-type-editor/creature-type-editor.component';
import {GetListInput} from '@app/tokens/get-list-input';

@Component({
  selector: 'rr-creature-types',
    imports: [
        GridComponent
    ],
  templateUrl: './creature-types.component.html',
  styleUrl: './creature-types.component.scss'
})
export class CreatureTypesComponent {
  headerActions: RRHeaderAction[] = [];
  columns: RRColumns[] = [];
  refreshGrid = new EventEmitter<void>();
  private campaign: Campaign;
  isMaster: boolean;
  private dialogRef: DynamicDialogRef<CreatureTypeEditorComponent>;


  constructor(private router: Router,
              private route: ActivatedRoute,
              private authenticationService: AuthenticationService,
              private dialogService: DialogService,
              public creatureTypeService: CreatureTypesService) {
    this.headerActions = this.buildHeaderActions();
    this.columns = this.buildColumns();
  }
  ngOnInit(): void {
    this.route.data.subscribe(data => {
      this.campaign = data['campaign'];
      this.isMaster = this.authenticationService.isMaster(this.campaign.masterId)
    });
  }
  getList = (input: GetListInput) => {
    return this.creatureTypeService.getList(this.campaign.campaignTemplate.id, input);
  }
  rowSelected(creatureType: CreatureType) {
    this.openCreatureTypeDialog(creatureType);
  }

  private openCreatureTypeDialog(creatureType: CreatureType): void {
    if (this.dialogRef) {
      this.dialogRef.close();
    }

    this.dialogRef = this.dialogService.open(CreatureTypeEditorComponent, {
      data: {
        campaign: this.campaign,
        creatureType: creatureType
      },
      position: 'right',
      height: '100%',
      width: '40vw',
      focusOnShow: false,
      closable: true,
      closeOnEscape: true,
      closeOnClickOutside: false,
      duplicate: true,
    } as DynamicDialogConfig);

    this.dialogRef.onClose.subscribe(() => {
      this.dialogRef = null;
    });
  }
  private buildHeaderActions() {
    return [
      {
        icon: 'pi pi-plus',
        condition: () => this.isMaster,
        tooltip: 'New',
        callBack: () => this.openCreatureTypeDialog(null),
      } as RRHeaderAction
    ];
  }

  private buildColumns() {
    return [{
      header: 'Name',
      property: 'name'
    } as RRColumns];
  }
}
