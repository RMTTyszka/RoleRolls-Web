import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup} from '@angular/forms';
import {LoginService} from '../login.service';
import {MessageService, ToastMessageOptions} from 'primeng/api';
import {AuthenticationService} from '../../authentication/authentication.service';
import {Router} from '@angular/router';
import {HttpErrorResponse} from '@angular/common/http';

@Component({
  selector: 'rr-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

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
      .subscribe(output => {
        if (output && !output.success) {
          this.messageService.add(<ToastMessageOptions>{
            summary: 'Error on user creation',
            detail: output.invalidPassword ? 'Invalid Password' : 'Username or Email alredy registered by another user',
            severity: 'error'});
        } else {
          this.messageService.add(<ToastMessageOptions>{
            summary: 'User successfully created',
            severity: 'success'});
        }
      }, error => {
      });
  }

  update() {
    this.service.updateUser(this.formCreated.get('email').value, this.form.get('password').value, this.formCreated.get('email').value).subscribe();
  }
}
