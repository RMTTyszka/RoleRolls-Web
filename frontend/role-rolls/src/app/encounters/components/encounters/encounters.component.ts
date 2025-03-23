import { Component, signal } from '@angular/core';
import { GridComponent, RRColumns, RRHeaderAction } from '@app/components/grid/grid.component';
import { Campaign } from '@app/campaigns/models/campaign';
import { DialogService, DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { EncounterEditorComponent } from '@app/encounters/components/encounter-editor/encounter-editor.component';
import { GetListInput } from '@app/tokens/get-list-input';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService } from '@app/authentication/services/authentication.service';
import { EncountersService } from '@app/encounters/services/encounters.service';
import { Encounter } from '@app/encounters/models/encounter';

@Component({
  selector: 'rr-encounters',
  imports: [
    GridComponent
  ],
  templateUrl: './encounters.component.html',
  styleUrl: './encounters.component.scss'
})
export class EncountersComponent {
  headerActions: RRHeaderAction[] = [];
  columns: RRColumns[] = [];
  refreshGrid = signal<boolean>(false);
  private campaign: Campaign;
  isMaster: boolean;
  private dialogRef: DynamicDialogRef<EncounterEditorComponent>;


  constructor(private router: Router,
              private route: ActivatedRoute,
              private authenticationService: AuthenticationService,
              private dialogService: DialogService,
              public encounterService: EncountersService) {
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
    return this.encounterService.getAll(this.campaign.id, input);
  }
  rowSelected(encounter: Encounter) {
    this.openEncounterDialog(encounter);
  }

  private openEncounterDialog(encounter: Encounter): void {
    if (this.dialogRef) {
      this.dialogRef.close();
    }

    this.dialogRef = this.dialogService.open(EncounterEditorComponent, {
      data: {
        campaign: this.campaign,
        encounter: encounter
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
        callBack: () => this.openEncounterDialog(null),
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
