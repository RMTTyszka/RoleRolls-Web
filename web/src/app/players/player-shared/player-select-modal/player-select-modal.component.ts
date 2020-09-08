import { Component, OnInit } from '@angular/core';
import {PlayersService} from '../../players.service';
import {Player} from '../../../shared/models/Player.model';
import {CmColumns} from '../../../shared/components/cm-grid/cm-grid.component';
import {DynamicDialogRef} from 'primeng/dynamicdialog';

@Component({
  selector: 'loh-player-select-modal',
  templateUrl: './player-select-modal.component.html',
  styleUrls: ['./player-select-modal.component.css']
})
export class PlayerSelectModalComponent implements OnInit {
  players: Player[] = [];
  cols: CmColumns[];
  constructor(
    private service: PlayersService,
    private dynamicDialogRef: DynamicDialogRef
  ) {
    this.cols = [
      {
        header: 'Name',
        property: 'name'
      }
    ];
  }

  ngOnInit(): void {
  }

  get(event: any) {
    this.service.getAllPlayers().subscribe(players => {
      this.players = players;
    });
  }

  heroSelected($event: any) {
    this.dynamicDialogRef.close($event.data);
  }
}
