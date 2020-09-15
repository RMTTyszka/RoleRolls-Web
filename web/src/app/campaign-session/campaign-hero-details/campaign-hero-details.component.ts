import {Component, Input, OnInit} from '@angular/core';
import {Hero} from '../../shared/models/NewHero.model';

@Component({
  selector: 'loh-campaign-hero-details',
  templateUrl: './campaign-hero-details.component.html',
  styleUrls: ['./campaign-hero-details.component.css']
})
export class CampaignHeroDetailsComponent implements OnInit {

  @Input() selectedHero: Hero;
  constructor() { }

  ngOnInit(): void {
  }

}
