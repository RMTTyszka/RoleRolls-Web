import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { CommonModule } from '@angular/common';
import { MainHeaderComponent } from "./home/main-header/main-header.component";
import { Tabs } from "primeng/tabs";
import { Toast } from 'primeng/toast';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, CommonModule, MainHeaderComponent, Toast],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'role-rolls';
}
