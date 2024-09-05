import {Component, Input, OnInit} from '@angular/core';
import {ItemTemplateModel} from "../../../../shared/models/pocket/itens/ItemTemplateModel";
import {EditorAction} from "../../../../shared/dtos/ModalEntityData";

@Component({
  selector: 'rr-campaign-item-creator',
  templateUrl: './campaign-item-creator.component.html',
  styleUrls: ['./campaign-item-creator.component.scss']
})
export class CampaignItemCreatorComponent implements OnInit {
  @Input() public item: ItemTemplateModel;
  @Input() public action: EditorAction;

  constructor() { }

  ngOnInit(): void {
  }

}
