import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup } from '@angular/forms';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { EditorAction } from 'src/app/shared/dtos/ModalEntityData';
import { createForm } from 'src/app/shared/EditorExtension';
import { PocketCampaignModel } from 'src/app/shared/models/pocket/campaigns/pocket.campaign.model';
import { AttributeTemplateModel } from 'src/app/shared/models/pocket/creature-templates/creature-template.model';
import { PocketCampaignsService } from '../pocket-campaigns.service';

@Component({
  selector: 'rr-campaign-creator',
  templateUrl: './campaign-creator.component.html',
  styleUrls: ['./campaign-creator.component.scss']
})
export class CampaignCreatorComponent implements OnInit {
  public action = EditorAction.create;
  public form = new FormGroup({});
  public attributeForm = new FormGroup({});
  public isLoading = true;
  public entity: PocketCampaignModel;
  public entityId: string;
  public requiredFields = ['name'];

  public get attributes(): FormArray {
    return this.form?.get('creatureTemplate.attributes') as FormArray;
  }

  constructor(
    public service: PocketCampaignsService,
    public ref: DynamicDialogRef,
    public formBuilder: FormBuilder,
    public config: DynamicDialogConfig,
  ) {
    this.action = config.data.action;
    this.entityId = config.data.entityId;
   }

  ngOnInit(): void {
  }


  public addAttribute() {
    const formArray = this.attributes;
    const newFormGroup = new FormGroup({});
    createForm(newFormGroup, this.attributeForm.value);
    formArray.controls.push(newFormGroup);
    this.attributeForm.reset();
  }
  loaded(entity: PocketCampaignModel) {
    this.isLoading = false;
    this.entity = entity;
    createForm(this.attributeForm, new AttributeTemplateModel());
  }
  deleted() {
    this.ref.close(true);
  }

  saved() {
    this.ref.close(true);
  }

}
