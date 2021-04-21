import {Component, Input, OnInit} from '@angular/core';
import {Creature} from '../../../shared/models/creatures/Creature.model';
import {ItemInstance} from '../../../shared/models/ItemInstance.model';
import {
  ItemInstantiatorModalData,
  ItemInstantiatorPath,
  MasterItemInstantiatorComponent
} from '../../../masters/master-tools/master-item-instantiator/master-item-instantiator.component';
import {DialogService} from 'primeng/dynamicdialog';
import {Campaign} from '../../../shared/models/campaign/Campaign.model';

@Component({
  selector: 'loh-creature-inventory-details',
  templateUrl: './creature-inventory-details.component.html',
  styleUrls: ['./creature-inventory-details.component.css']
})
export class CreatureInventoryDetailsComponent implements OnInit {
  @Input() public creature: Creature;
  @Input() public isMaster = false;
  @Input() public campaign: Campaign;
  public get itens(): ItemInstance[] {
    return this.creature.inventory.items;
  }
  constructor(
    private dialog: DialogService,
  ) { }

  ngOnInit(): void {
  }
  selectItemTemplate() {
    this.dialog.open(MasterItemInstantiatorComponent, {
      data: {
        creatureId: this.creature.id,
        campaignId: this.campaign.id,
        path: ItemInstantiatorPath.campaign
      } as ItemInstantiatorModalData
    }).onClose.subscribe((creature: Creature) => {
      this.creature = creature;
    });
  }

}
