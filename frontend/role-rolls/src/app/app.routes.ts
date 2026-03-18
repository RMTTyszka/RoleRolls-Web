import { Route, Routes } from '@angular/router';
import { campaignResolver } from './campaigns/campaign-details/services/campaign.resolver';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./home/home.component').then((m) => m.HomeComponent)
  } as Route,
  {
    path: 'login',
    loadComponent: () => import('./authentication/login/login.component').then((m) => m.LoginComponent)
  } as Route,
  {
    path: 'campaigns/:campaignId',
    loadComponent: () => import('./campaigns/campaign-details/campaign-details/campaign-details.component').then((m) => m.CampaignDetailsComponent),
    resolve: {
      campaign: campaignResolver
    }
  } as Route,
  {
    path: 'campaigns/:campaignId/session',
    loadComponent: () => import('./campaign-session/campaign-session.component').then((m) => m.CampaignSessionComponent),
    resolve: {
      campaign: campaignResolver
    }
  } as Route,
  {
    path: 'newCampaign',
    loadComponent: () => import('./campaigns/campaign-creator/campaign-creator.component').then((m) => m.CampaignCreatorComponent)
  } as Route,
];
