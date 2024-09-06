import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { map, tap } from 'rxjs/operators';
import { createForm } from 'src/app/shared/EditorExtension';
import { PocketCampaignModel } from 'src/app/shared/models/pocket/campaigns/pocket.campaign.model';
import { PocketCreature } from 'src/app/shared/models/pocket/creatures/pocket-creature';
import { CreatureType } from 'src/app/shared/models/creatures/CreatureType';
import {PocketCampaignsService} from "../../pocket-campaigns.service";

@Component({
  selector: 'rr-pocket-creature-select',
  templateUrl: './pocket-creature-select.component.html',
  styleUrls: ['./pocket-creature-select.component.scss']
})
export class PocketCreatureSelectComponent implements OnInit {
  @Input() campaign: PocketCampaignModel;
  @Input() placeholder: string;
  @Input() creatureType: CreatureType;
  @Output() creatureSelected = new EventEmitter<PocketCreature>();
  result: string[] = [];
  creatures: PocketCreature[] = [];
  value: string;
  constructor(
    private readonly campaignsService: PocketCampaignsService
  ) { }

  ngOnInit(): void {
  }

  search(event) {
    this.campaignsService.getCreatures(this.campaign.id, this.creatureType).pipe(
      tap(resp => this.creatures = resp),
      map(resp => resp.map(creature => creature.name))
          ).subscribe(response => this.result = response);
  }
  selected(creatureName: string) {
    const selectedCreature = this.creatures.find(r => r.name === creatureName);
    const form = new FormGroup({});
    createForm(form , selectedCreature);
    this.creatureSelected.emit(selectedCreature);
  }
}
