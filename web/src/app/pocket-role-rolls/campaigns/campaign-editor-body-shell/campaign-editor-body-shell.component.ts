import { Component, OnInit } from '@angular/core';
import {DynamicDialogConfig} from "primeng/dynamicdialog";
import {EditorAction} from "../../../shared/dtos/ModalEntityData";

@Component({
  selector: 'rr-campaign-editor-body-shell',
  templateUrl: './campaign-editor-body-shell.component.html',
  styleUrls: ['./campaign-editor-body-shell.component.scss']
})
export class CampaignEditorBodyShellComponent implements OnInit {
  public action = EditorAction.create;
  public entityId: string;
  constructor(
    public config: DynamicDialogConfig,

  ) {
    this.action = config.data.action;
    this.entityId = config.data.entityId;
  }

  ngOnInit(): void {

  }

}
