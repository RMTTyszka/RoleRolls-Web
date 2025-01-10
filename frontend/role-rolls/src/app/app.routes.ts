import { Route, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './authentication/login/login.component';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => HomeComponent
  } as Route,
  {
    path: 'login',
    loadComponent: () => LoginComponent
  } as Route,
];
