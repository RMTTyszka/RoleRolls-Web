import { Component, OnInit } from '@angular/core';
import {BreakpointObserver, BreakpointState} from '@angular/cdk/layout';

@Component({
  selector: 'loh-items',
  templateUrl: './items.component.html',
  styleUrls: ['./items.component.css']
})
export class ItemsComponent implements OnInit {
  private subscriptions = {};
  isSmallScreen: boolean;

  constructor(
    private breakpointObserver: BreakpointObserver
  ) { }

  ngOnInit() {
    this.subscriptions['isSmallScreen'] = this.breakpointObserver.observe(['(max-width: 599px)'])
      .subscribe((state: BreakpointState) => {
        this.isSmallScreen = state.matches;
      });
  }

}
