import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {RollResult} from '../../shared/models/RollResult.model';
import {LOH_API} from '../../loh.api';
import {FormBuilder} from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class MakeTestService {
  myBackUrl: string = LOH_API.myBackUrl;

  constructor(
    private http: HttpClient,
  ) {
  }

  makeTest(level, bonus, difficulty, complexity): Observable<RollResult> {
    return this.http.get<RollResult>(this.myBackUrl + 'roll/makeTest',
      {params: {level: level, bonus: bonus, difficulty: difficulty, complexity: complexity}});
  }
}
