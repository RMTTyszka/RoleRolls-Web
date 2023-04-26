import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Subject } from 'rxjs';
import { getAsForm } from '../../../../shared/EditorExtension';
import { CampaignScene } from '../../../../shared/models/pocket/campaigns/campaign-scene-model';
import { PocketCampaignModel } from '../../../../shared/models/pocket/campaigns/pocket.campaign.model';
import { RollInput } from '../../models/RollInput';
import { PocketCampaignsService } from '../../pocket-campaigns.service';
import { switchMap } from 'rxjs/operators';
import { PocketRoll } from '../../models/pocket-roll.model';
import { PocketCampaignDetailsService } from 'src/app/pocket-role-rolls/campaigns/pocket-campaign-bodyshell/pocket-campaign-details.service';

@Component({
  selector: 'rr-roll-dice',
  templateUrl: './roll-dice.component.html',
  styleUrls: ['./roll-dice.component.scss']
})
export class RollDiceComponent implements OnInit {

  @Input() public campaign: PocketCampaignModel;
  @Input() public scene: CampaignScene;
  public rollInput: RollInput;
  public form: FormGroup;
  public rollResult: PocketRoll;

  constructor(
    private readonly campaignsService: PocketCampaignsService,
    private readonly detailsService: PocketCampaignDetailsService
  ) { }

  ngOnInit(): void {
    this.detailsService.rollInputEmitter.subscribe((rollInput: RollInput) => {
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
      this.detailsService.rollResultEmitter.next(true);
    });
  }
  public cleanRolls() {
    this.form.get('rollsAsString').reset();
  }

}
