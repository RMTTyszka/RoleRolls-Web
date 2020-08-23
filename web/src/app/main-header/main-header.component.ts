import { Component, OnInit } from '@angular/core';
import {AuthenticationService} from '../authentication/authentication.service';
import {LoginService} from '../login/login.service';

@Component({
  selector: 'loh-main-header',
  templateUrl: './main-header.component.html',
  styleUrls: ['./main-header.component.css']
})
export class MainHeaderComponent implements OnInit {
  title = 'Land Of Heroes';
  userName: string;

  get hasUser() {
    return this.authService.isLogged;
  }
  constructor(
    private readonly authService: AuthenticationService
  ) {
    this.authService.userNameChanged.subscribe(userName => this.userName = userName);
    this.authService.getUser();
  }

  ngOnInit(): void {
  }
  logout() {
    this.authService.cleanTokenAndUserName();
  }


}
