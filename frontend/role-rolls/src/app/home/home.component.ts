import { Component } from '@angular/core';
import { Tab, TabList, TabPanel, TabPanels, Tabs } from 'primeng/tabs';
import { MainHeaderComponent } from './main-header/main-header.component';
import { CampaignListComponent } from '../campaigns/campaign-list/campaign-list.component';

@Component({
  selector: 'rr-home',
  imports: [
    Tab,
    TabList,
    TabPanel,
    TabPanels,
    Tabs,
    MainHeaderComponent,
    CampaignListComponent
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {

}
