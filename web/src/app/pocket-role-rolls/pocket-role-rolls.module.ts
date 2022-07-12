import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PocketLoginComponent } from './pocket-login/pocket-login.component';
import { LoginModule } from '../login/login.module';
import { LoginService } from '../login/login.service';
import { PocketLoginService } from './pocket-login.service';
import { PocketHomeComponent } from './pocket-home/pocket-home.component';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from '../login/login/login.component';

const routes: Routes = [
  {path: 'campaings', loadChildren: () => import('./campaigns/campaigns.module').then(m => m.CampaignsModule)},
  {path: 'login', component: LoginComponent},
  {path: '', redirectTo: '/login', pathMatch: 'full'},
  {path: '**', redirectTo: ''},

];

@NgModule({
  declarations: [
    PocketLoginComponent,
    PocketHomeComponent,
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    LoginModule
  ],
  providers: [
    { provide: LoginService, useClass: PocketLoginService}
  ]
})
export class PocketRoleRollsModule { }
