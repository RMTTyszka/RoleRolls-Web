import {Injectable} from '@angular/core';
import {Observable} from 'rxjs';
import {HttpClient} from '@angular/common/http';
import {LOH_API} from '../loh.api';
import {Player} from '../shared/models/Player.model';

@Injectable({
  providedIn: 'root'
})
export class PlayersService {

  constructor(
    private http: HttpClient
  ) { }

  getAllPlayers(): Observable<Player[]> {
    return this.http.get<Player[]>(LOH_API.myBackUrl + 'context/allPlayers');
  }
}
