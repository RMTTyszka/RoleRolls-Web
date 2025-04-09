import {Component, EventEmitter, signal} from '@angular/core';
import {GridComponent, RRColumns, RRHeaderAction} from "@app/components/grid/grid.component";
import {ActivatedRoute, Router} from '@angular/router';
import {Campaign} from '@app/campaigns/models/campaign';
import {AuthenticationService} from '@app/authentication/services/authentication.service';
import { DialogService, DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import {GetListInput} from '@app/tokens/get-list-input';
import {
  ArchetypeEditorComponent
} from '@app/campaigns/campaign-details/archetypes/archetype-editor/archetype-editor.component';
import { ArchetypesService } from '@services/archetypes/archetypes.service';
import { Archetype } from '@app/models/archetypes/archetype';

@Component({
  selector: 'rr-archetypes',
    imports: [
        GridComponent
    ],
  templateUrl: './archetypes.component.html',
  styleUrl: './archetypes.component.scss'
})
export class ArchetypesComponent {
  headerActions: RRHeaderAction[] = [];
  columns: RRColumns[] = [];
  refreshGrid = new EventEmitter<void>();
  private campaign: Campaign;
  isMaster: boolean;
  private dialogRef: DynamicDialogRef<ArchetypeEditorComponent>;


  constructor(private router: Router,
              private route: ActivatedRoute,
              private authenticationService: AuthenticationService,
              private dialogService: DialogService,
              public archetypeService: ArchetypesService) {
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
    return this.archetypeService.getList(this.campaign.campaignTemplate.id, input);
  }
  rowSelected(archetype: Archetype) {
    this.openArchetypeDialog(archetype);
  }

  private openArchetypeDialog(archetype: Archetype): void {
    if (this.dialogRef) {
      this.dialogRef.close();
    }

    this.dialogRef = this.dialogService.open(ArchetypeEditorComponent, {
      data: {
        campaign: this.campaign,
        archetypeId: archetype.id
      },
      position: 'right',
      height: '100%',
      width: '90vw',
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
        callBack: () => this.openArchetypeDialog(null),
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
