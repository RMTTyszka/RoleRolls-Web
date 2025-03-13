import { Component, EventEmitter, Input, Output } from '@angular/core';
import { AutoComplete } from 'primeng/autocomplete';
import { Campaign } from '@app/campaigns/models/campaign';
import { tap } from 'rxjs/operators';
import { CreatureType } from '@app/models/creatureTypes/creature-type';
import { Creature } from '@app/campaigns/models/creature';
import { CampaignsService } from '@app/campaigns/services/campaigns.service';
import { CreatureCategory } from '@app/campaigns/models/CreatureCategory';

@Component({
  selector: 'rr-creature-select',
  imports: [
    AutoComplete
  ],
  templateUrl: './creature-select.component.html',
  styleUrl: './creature-select.component.scss',
  host: {
    class: 'flex',
  }
})
export class CreatureSelectComponent {
  @Input() campaign: Campaign;
  @Input() placeholder: string;
  @Input() creatureType: CreatureCategory;
  @Input() creaturesAvailable: Creature[] = [];
  @Output() creatureSelected = new EventEmitter<Creature>();
  result: Creature[] = [];
  creatures: Creature[] = [];
  constructor(
    private readonly campaignsService: CampaignsService
  ) { }

  ngOnInit(): void {
  }

  search( ) {
    if (this.creaturesAvailable && this.creaturesAvailable.length > 0) {
      this.result = [...this.creaturesAvailable];
      return;
    }
    this.campaignsService.getCreatures(this.campaign.id, this.creatureType).pipe(
      tap(resp => this.creatures = resp.items),
    ).subscribe(response => this.result = response.items);
  }
  selected(selectedCreature: Creature) {
    this.creatureSelected.emit(selectedCreature);
  }
}
