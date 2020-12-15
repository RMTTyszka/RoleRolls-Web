import { Component, OnInit } from '@angular/core';
import {CampaignsService} from '../campaigns.service';
import {RRColumns} from '../../shared/components/cm-grid/cm-grid.component';
import {DialogService} from 'primeng/dynamicdialog';
import {EditorAction, ModalEntityData} from '../../shared/dtos/ModalEntityData';
import {Campaign} from '../../shared/models/campaign/Campaign.model';
import {CampaignEditorComponent} from '../campaign-editor/campaign-editor.component';
import {CampaingsConfig} from '../campaings-config';

@Component({
  selector: 'loh-campaign-list',
  templateUrl: './campaign-list.component.html',
  styleUrls: ['./campaign-list.component.css'],
  providers: [DialogService]
})
export class CampaignListComponent implements OnInit {
  columns: RRColumns[] = [
    {
      header: 'Name',
      property: 'name'
    },
    {
      header: 'Resume',
      property: 'description'
    },
  ];
  config = new CampaingsConfig();

  constructor(
    public dialog: DialogService,
    public campaignsService: CampaignsService
  ) { }

  ngOnInit(): void {
  }


  create() {
    this.openEditor(EditorAction.create).onClose.subscribe();
  }

  update(campaign: Campaign) {
    this.openEditor(EditorAction.update, campaign)
      .onClose.subscribe();
  }

  openEditor(action: EditorAction, campaign?: Campaign) {
    return this.dialog.open(CampaignEditorComponent, {
      data: <ModalEntityData<Campaign>> {
        entity: campaign,
        action: action
      },
      header: 'Campaign',
      width: '100',
      height: '100'

    });
  }

  campaignSelected(campaign: Campaign) {
    this.update(campaign);
  }

}
