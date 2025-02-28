import { Component, Input } from '@angular/core';
import { RollInput } from '@app/campaigns/models/RollInput';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { getAsForm } from '@app/tokens/EditorExtension';
import { Subject } from 'rxjs';
import { CampaignScene } from '@app/campaigns/models/campaign-scene-model';
import { Campaign } from '@app/campaigns/models/campaign';
import { Roll } from '@app/campaigns/models/pocket-roll.model';
import { CampaignsService } from '@app/campaigns/services/campaigns.service';
import { InputNumber } from 'primeng/inputnumber';
import { NgIf } from '@angular/common';
import { InputText } from 'primeng/inputtext';
import { ButtonDirective } from 'primeng/button';

@Component({
  selector: 'rr-roll-dice',
  imports: [
    ReactiveFormsModule,
    InputNumber,
    NgIf,
    InputText,
    ButtonDirective
  ],
  templateUrl: './roll-dice.component.html',
  styleUrl: './roll-dice.component.scss'
})
export class RollDiceComponent {

  @Input() public rollInputEmitter: Subject<RollInput>;
  @Input() public rollResultEmitter: Subject<boolean>;
  @Input() public campaign: Campaign;
  @Input() public scene: CampaignScene;
  public rollInput: RollInput;
  public form: FormGroup;
  public rollResult: Roll;

  constructor(
    private readonly campaignsService: CampaignsService
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
      this.form = getAsForm(rollInput, false, [], ['propertyName']);
    });
  }

  public roll() {
    const rollInput = this.form.value as RollInput;
    rollInput.rolls = rollInput.rollsAsString ? rollInput.rollsAsString.split(',').map(a => Number(a)) : [];
    this.campaignsService.rollForCreature(this.campaign.id, this.scene.id, rollInput.creatureId, rollInput)
      .subscribe((roll: Roll) => {
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
