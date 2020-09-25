import {Component, OnInit} from '@angular/core';
import {Player} from '../../../shared/models/Player.model';
import {RRColumns} from '../../../shared/components/cm-grid/cm-grid.component';
import {CampaignsService} from '../../../campaign/campaigns.service';
import {DynamicDialogConfig, DynamicDialogRef} from 'primeng/dynamicdialog';
import {Hero} from '../../../shared/models/NewHero.model';
import {HeroesService} from '../../heroes.service';

@Component({
  selector: 'loh-heroes-select-modal',
  templateUrl: './heroes-select-modal.component.html',
  styleUrls: ['./heroes-select-modal.component.css']
})
export class HeroesSelectModalComponent implements OnInit {
  heroes: Hero[] = [];
  cols: RRColumns[];
  campaignId: string;
  constructor(
    private service: HeroesService,
    private dynamicDialogRef: DynamicDialogRef,
    private dialogConfig: DynamicDialogConfig,
  ) {
    this.cols = [
      {
        header: 'Name',
        property: 'name'
      }
    ];
  }

  ngOnInit(): void {
    this.campaignId = this.dialogConfig.data.campaignId;
  }

  get(event: any) {
    this.service.getAllFiltered(null, 0,10).subscribe(heroes => {
      this.heroes = heroes;
    });
  }

  heroSelected($event: any) {
    this.dynamicDialogRef.close($event.data);
  }
}
