import { Component, OnInit } from '@angular/core';
import {PlayersService} from '../../players.service';
import {Player} from '../../../shared/models/Player.model';
import {RRColumns} from '../../../shared/components/cm-grid/cm-grid.component';
import {DynamicDialogRef} from 'primeng/dynamicdialog';

@Component({
  selector: 'rr-player-select-modal',
  templateUrl: './player-select-modal.component.html',
  styleUrls: ['./player-select-modal.component.css']
})
export class PlayerSelectModalComponent implements OnInit {
  players: Player[] = [];
  cols: RRColumns[];
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
