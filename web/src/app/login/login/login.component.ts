import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup} from '@angular/forms';
import {LoginService} from '../login.service';

@Component({
  selector: 'loh-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  form: FormGroup
  formCreated: FormGroup;
  constructor(
    private fb: FormBuilder,
    private service: LoginService
  ) {
    this.form = this.fb.group({
      username: [],
      password: []
    });
    this.formCreated = this.fb.group({
      username: [],
      password: [],
      email: []
    });
  }

  ngOnInit(): void {

  }

  login() {
    this.service.login(this.form.get('username').value, this.form.get('password').value).subscribe();
  }
  add() {
    this.service.addUser(this.formCreated.get('email').value, this.formCreated.get('password').value, this.formCreated.get('email').value).subscribe();
  }

  update() {
    this.service.updateUser(this.formCreated.get('email').value, this.form.get('password').value, this.formCreated.get('email').value).subscribe();
  }
}
