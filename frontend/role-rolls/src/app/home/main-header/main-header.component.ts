import { Component, Inject, ChangeDetectionStrategy } from '@angular/core';
import { AuthenticationService } from '../../authentication/services/authentication.service';
import { SelectItem } from 'primeng/api';
import { NgIf, TitleCasePipe } from '@angular/common';
import { RouterLink } from '@angular/router';
import { ButtonModule, ButtonIcon } from 'primeng/button';

@Component({
  selector: 'rr-main-header',
  imports: [
    TitleCasePipe,
    NgIf,
    RouterLink,
    ButtonModule,
    ButtonIcon
  ],
  templateUrl: './main-header.component.html',
  changeDetection: ChangeDetectionStrategy.Eager,
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

  public toggleDarkMode() {
    const element = document.querySelector('html');
    element.classList.toggle('my-app-dark');
  }
}

