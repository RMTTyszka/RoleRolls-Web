import { Component, Inject } from '@angular/core';
import { AuthenticationService } from '../../authentication/services/authentication.service';
import { SelectItem } from 'primeng/api';
import { NgIf, TitleCasePipe } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'rr-main-header',
  imports: [
    TitleCasePipe,
    NgIf,
    RouterLink
  ],
  templateUrl: './main-header.component.html',
  styleUrl: './main-header.component.scss'
})
export class MainHeaderComponent {
  title = 'Role Rolls';
  userName: string | null = null;
  universeOptions: SelectItem[] = [];

  get hasUser() {
    return this.authService.isLogged;
  }

  constructor(
    private readonly authService: AuthenticationService,
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

