import { Route, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './authentication/login/login.component';
import { CampaignDetailsComponent } from './campaigns/campaign-details/campaign-details/campaign-details.component';
import { CampaignCreatorComponent } from './campaigns/campaign-creator/campaign-creator.component';
import { campaignResolver } from './campaigns/campaign-details/services/campaign.resolver';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => HomeComponent
  } as Route,
  {
    path: 'login',
    loadComponent: () => LoginComponent
  } as Route,
  {
    path: 'campaigns/:campaignId',
    loadComponent: () => CampaignDetailsComponent,
    resolve: {
      campaign: campaignResolver
    }
  } as Route,
  {
    path: 'newCampaign',
    loadComponent: () => CampaignCreatorComponent
  } as Route,
];
