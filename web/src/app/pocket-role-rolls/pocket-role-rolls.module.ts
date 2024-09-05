import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PocketLoginComponent } from './pocket-login/pocket-login.component';
import { LoginModule } from '../login/login.module';
import { LoginService } from '../login/login.service';
import { PocketLoginService } from './pocket-login.service';
import { PocketHomeComponent } from './pocket-home/pocket-home.component';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from '../login/login/login.component';
import { PocketCreatureEditorComponent } from './pocket-creature-editor/pocket-creature-editor.component';
import { CanActivateAuthGuard } from 'src/app/pocket-role-rolls/pocket-login/auth-guard';
import { AuthGuardGuard } from './auth-guard.guard';

const routes: Routes = [
  {path: 'campaigns', loadChildren: () => import('./campaigns/campaigns.module').then(m => m.CampaignsModule),
    canActivateChild: [CanActivateAuthGuard]},
  {path: 'login', component: LoginComponent},
  {path: '', redirectTo: 'pocket/campaigns', pathMatch: 'full'},
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
    { provide: LoginService, useClass: PocketLoginService }
  ]
})
export class PocketRoleRollsModule { }
