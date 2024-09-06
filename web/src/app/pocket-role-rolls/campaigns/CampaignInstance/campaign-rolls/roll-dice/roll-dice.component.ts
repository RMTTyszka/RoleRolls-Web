import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Subject } from 'rxjs';
import { getAsForm } from 'src/app/shared/EditorExtension';
import { CampaignScene } from 'src/app/shared/models/pocket/campaigns/campaign-scene-model';
import { PocketCampaignModel } from 'src/app/shared/models/pocket/campaigns/pocket.campaign.model';
import {RollInput} from "../../../models/RollInput";
import {PocketRoll} from "../../../models/pocket-roll.model";
import {PocketCampaignsService} from "../../../pocket-campaigns.service";

@Component({
  selector: 'rr-roll-dice',
  templateUrl: './roll-dice.component.html',
  styleUrls: ['./roll-dice.component.scss']
})
export class RollDiceComponent implements OnInit {

  @Input() public rollInputEmitter: Subject<RollInput>;
  @Input() public rollResultEmitter: Subject<boolean>;
  @Input() public campaign: PocketCampaignModel;
  @Input() public scene: CampaignScene;
  public rollInput: RollInput;
  public form: FormGroup;
  public rollResult: PocketRoll;

  constructor(
    private readonly campaignsService: PocketCampaignsService
  ) { }

  ngOnInit(): void {
    this.rollInputEmitter.subscribe((rollInput: RollInput) => {
      this.rollInput = rollInput;
      this.rollInput.complexity = 0;
      this.rollInput.difficulty = 0;
      this.rollInput.propertyBonus = 0;
      this.rollInput.rollBonus = 0;
      this.rollInput.rollsAsString = '';
      this.rollInput.description = '';
      this.form = getAsForm(rollInput, [], ['propertyName']);
    });
  }

  public roll() {
    const rollInput = this.form.value as RollInput;
    rollInput.rolls = rollInput.rollsAsString ? rollInput.rollsAsString.split(',').map(a => Number(a)) : [];
    this.campaignsService.rollForCreature(this.campaign.id, this.scene.id, rollInput.creatureId, rollInput)
    .subscribe((roll: PocketRoll) => {
      this.rollResult = roll;
      if (this.rollResultEmitter) {
        this.rollResultEmitter.next(true);
      }
    });
  }
  public cleanRolls() {
    this.form.get('rollsAsString').reset();
  }

}
