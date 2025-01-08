import { Component } from '@angular/core';
import { Tab, TabList, TabPanel, TabPanels, Tabs } from 'primeng/tabs';
import { MainHeaderComponent } from './main-header/main-header.component';

@Component({
  selector: 'rr-home',
  imports: [
    Tab,
    TabList,
    TabPanel,
    TabPanels,
    Tabs,
    MainHeaderComponent
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {

}
