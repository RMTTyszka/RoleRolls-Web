import { Component, output } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { LoginService } from './services/login.service';
import { Router } from '@angular/router';
import { AuthenticationService } from '../services/authentication.service';
import { MessageService, ToastMessageOptions } from 'primeng/api';
import { HttpErrorResponse } from '@angular/common/http';
import { Panel } from 'primeng/panel';
import { CommonModule, NgIf } from '@angular/common';

@Component({
  selector: 'rr-login',
  imports: [
    ReactiveFormsModule,
    Panel,
    CommonModule
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {

  form: FormGroup;
  formCreated: FormGroup;
  get isLogged() {
    return this.authService.isLogged;
  }
  constructor(
    private fb: FormBuilder,
    private service: LoginService,
    private router: Router,
    private authService: AuthenticationService,
    private messageService: MessageService
  ) {
    this.form = this.fb.group({
      /*       password: [''],
            email: [''] */
      password: [],
      email: []
    });
    this.formCreated = this.fb.group({
      firstName: [],
      lastName: [],
      userName: [],
      /*       password: [''],
            email: [''] */
      password: [],
      login: [],
      email: []
    });
  }

  ngOnInit(): void {

  }

  login() {
    this.service.login(this.form.get('email').value, this.form.get('password').value)
      .subscribe(output => {
        this.authService.setToken(output.token);
        this.authService.publishNewUserName(output.userName, output.userId);
        this.messageService.add(<ToastMessageOptions>{
          summary: 'Logged in',
          severity: 'success'});
        this.router.navigateByUrl(this.authService.lastRoute);
      }, (error: HttpErrorResponse) => {
        if (error.status === 401) {
          this.messageService.add(<ToastMessageOptions>{
            summary: 'Error on user creation',
            detail: 'Invalid Email or Password',
            severity: 'error'});
        }
      });
  }
  add() {
    this.service.addUser(this.formCreated.getRawValue())
      .subscribe(() => {
          this.messageService.add(<ToastMessageOptions>{
            summary: 'User successfully created',
            severity: 'success'});
      }, error => {
        this.messageService.add(<ToastMessageOptions>{
          summary: 'Error on user creation',
          detail: error.invalidPassword ? 'Invalid Password' : 'Username or Email alredy registered by another user',
          severity: 'error'});
      });
  }

  update() {
    this.service.updateUser(this.formCreated.get('email').value, this.form.get('password').value, this.formCreated.get('email').value).subscribe();
  }
}
