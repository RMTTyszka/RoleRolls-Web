import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { map, tap } from 'rxjs/operators';
import { createForm } from 'src/app/shared/EditorExtension';
import { PocketCampaignModel } from 'src/app/shared/models/pocket/campaigns/pocket.campaign.model';
import { PocketCreature } from 'src/app/shared/models/pocket/creatures/pocket-creature';
import { CreatureType } from 'src/app/shared/models/creatures/CreatureType';
import {PocketCampaignsService} from "../../pocket-campaigns.service";
import {AutoCompleteModule} from 'primeng/autocomplete';

@Component({
  selector: 'rr-pocket-creature-select',
  standalone: true,
  templateUrl: './pocket-creature-select.component.html',
  imports: [
    AutoCompleteModule
  ],
  styleUrls: ['./pocket-creature-select.component.scss']
})
export class PocketCreatureSelectComponent implements OnInit {
  @Input() campaign: PocketCampaignModel;
  @Input() placeholder: string;
  @Input() creatureType: CreatureType;
  @Input() creaturesAvailable: PocketCreature[] = [];
  @Output() creatureSelected = new EventEmitter<PocketCreature>();
  result: PocketCreature[] = [];
  creatures: PocketCreature[] = [];
  constructor(
    private readonly campaignsService: PocketCampaignsService
  ) { }

  ngOnInit(): void {
  }

  search(event) {
    if (this.creaturesAvailable && this.creaturesAvailable.length > 0) {
      this.result = [...this.creaturesAvailable];
         return;
    }
    this.campaignsService.getCreatures(this.campaign.id, this.creatureType).pipe(
      tap(resp => this.creatures = resp),
          ).subscribe(response => this.result = response);
  }
  selected(selectedCreature: PocketCreature) {
    this.creatureSelected.emit(selectedCreature);
  }
}
