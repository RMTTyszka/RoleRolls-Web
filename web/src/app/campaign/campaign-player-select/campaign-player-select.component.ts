import { Component, OnInit } from '@angular/core';
import {Player} from '../../shared/models/Player.model';
import {CmColumns} from '../../shared/components/cm-grid/cm-grid.component';
import {PlayersService} from '../../players/players.service';
import {DynamicDialogConfig, DynamicDialogRef} from 'primeng/dynamicdialog';
import {CampaignsService} from '../campaigns.service';

@Component({
  selector: 'loh-campaign-player-select',
  templateUrl: './campaign-player-select.component.html',
  styleUrls: ['./campaign-player-select.component.css']
})
export class CampaignPlayerSelectComponent implements OnInit {
  players: Player[] = [];
  cols: CmColumns[];
  campaignId: string;
  constructor(
    private service: CampaignsService,
    private dynamicDialogRef: DynamicDialogRef,
    private dialogConfig: DynamicDialogConfig,
  ) {
    this.cols = [
      {
        header: 'Name',
        property: 'name'
      }
    ];
  }

  ngOnInit(): void {
    this.campaignId = this.dialogConfig.data.campaignId;
  }

  get(event: any) {
    this.service.getPlayer(this.campaignId, 0,10).subscribe(players => {
      this.players = players;
    });
  }

  heroSelected($event: any) {
    this.dynamicDialogRef.close($event.data);
  }
}
