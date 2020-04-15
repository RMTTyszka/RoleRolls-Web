import {Component, OnInit} from '@angular/core';
import {PlayersService} from '../players.service';
import {Player} from '../../shared/models/Player.model';

@Component({
  selector: 'loh-players',
  templateUrl: './players.component.html',
  styleUrls: ['./players.component.css']
})
export class PlayersComponent implements OnInit {

  players: Player[];

  constructor(private _playersService: PlayersService) { }

  ngOnInit() {
    this._playersService.getAllPlayers().subscribe(data => {
      this.players = data;
    });
  }

}
