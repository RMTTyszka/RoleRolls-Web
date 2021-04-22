import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup} from '@angular/forms';
import {LoginService} from '../login.service';
import {Message, MessageService} from 'primeng/api';
import {AuthenticationService} from '../../authentication/authentication.service';
import {LohAuthTokenName} from '../../authentication/AuthTokens';
import {Router} from '@angular/router';

@Component({
  selector: 'rr-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  form: FormGroup
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
      password: ['hell666'],
      email: ['admin@rolerolls.com']
    });
    this.formCreated = this.fb.group({
      firstName: [],
      lastName: [],
      userName: [],
      password: ['hell666'],
      email: ['admin@rolerolls.com']
    });
  }

  ngOnInit(): void {

  }

  login() {
    this.service.login(this.form.get('email').value, this.form.get('password').value)
      .subscribe(output => {
        this.authService.setToken(output.token);
        this.authService.publishNewUserName(output.userName, output.userId);
        this.messageService.add(<Message>{
          summary: 'Logged in',
          severity: 'success'});
        this.router.navigateByUrl(this.authService.lastRoute);
      });
  }
  add() {
    this.service.addUser(this.formCreated.getRawValue())
      .subscribe(output => {
        if (!output.success) {
          this.messageService.add(<Message>{
            summary: 'Error on user creation',
            detail: output.invalidPassword ? 'Invalid Password' : 'Username or Email alredy registered by another user',
            severity: 'error'});
        } else {
          this.messageService.add(<Message>{
            summary: 'User successfully created',
            severity: 'success'});
        }
      });
  }

  update() {
    this.service.updateUser(this.formCreated.get('email').value, this.form.get('password').value, this.formCreated.get('email').value).subscribe();
  }
}
