import {Component, Input} from '@angular/core';
import {RollInput} from '@app/campaigns/models/RollInput';
import {FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import {getAsForm} from '@app/tokens/EditorExtension';
import {Subject} from 'rxjs';
import {CampaignScene} from '@app/campaigns/models/campaign-scene-model';
import {Campaign} from '@app/campaigns/models/campaign';
import {Roll} from '@app/campaigns/models/pocket-roll.model';
import {CampaignsService} from '@app/campaigns/services/campaigns.service';
import {InputNumber} from 'primeng/inputnumber';
import {NgIf} from '@angular/common';
import {InputText} from 'primeng/inputtext';
import {ButtonDirective} from 'primeng/button';
import {FormFieldWrapperComponent} from '@app/components/form-field-wrapper/form-field-wrapper.component';
import {FieldTitleDirective} from '@app/components/form-field-wrapper/field-title.directive';
import {AutoCompleteModule} from 'primeng/autocomplete';
import {IntegerOnlyDirective} from '@app/directives/integer-only.directive';
import {AdvantageSelectComponent} from '@app/rolls/advantage-select/advantage-select.component';
import {LuckSelectComponent} from '@app/rolls/luck-select/luck-select.component';
import {PropertyType} from '@app/campaigns/models/propertyType';
import {PropertySelectorComponent} from '@app/components/property-selector/property-selector.component';
import {Property} from '@app/models/bonuses/bonus';

@Component({
  selector: 'rr-roll-dice',
  imports: [
    ReactiveFormsModule,
    InputNumber,
    NgIf,
    InputText,
    ButtonDirective,
    FormFieldWrapperComponent,
    FieldTitleDirective,
    AutoCompleteModule,
    IntegerOnlyDirective,
    AdvantageSelectComponent,
    LuckSelectComponent,
    PropertySelectorComponent,

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
  public hasProperty: boolean;
  public propertyType = PropertyType;

  constructor(
    private readonly campaignsService: CampaignsService
  ) { }

  ngOnInit(): void {
    this.rollInputEmitter.subscribe((rollInput: RollInput) => {
      this.rollInput = rollInput;
      this.rollInput.complexity = 0;
      this.rollInput.difficulty = 0;
      this.rollInput.advantage = 0;
      this.rollInput.luck = 0;
      this.rollInput.bonus = 0;
      this.rollInput.rollsAsString = null;
      this.rollInput.description = '';
      this.rollInput.attribute = null;
      this.hasProperty = Boolean(this.rollInput.propertyName);
      this.form = getAsForm(rollInput, {
        disabledFields: ['propertyName']
      });
      if (this.rollInput.property.type === PropertyType.SpecificSkill) {
        const specificSkill = this.rollInput.creature.attributelessSkills.flatMap(s => s.specificSkills)
          .find(s => s.id === this.rollInput.property.id);
        if (specificSkill && !specificSkill.attributeId) {
          this.form.get('attribute').addValidators(Validators.required);
        } else {
          this.form.get('attribute').setValue( {
            type: PropertyType.Attribute,
            id: specificSkill.attributeId,
          } as Property);
        }
      } else if (this.rollInput.property.type === PropertyType.Skill) {
        const skill = this.rollInput.creature.skills
          .find(s => s.id === this.rollInput.property.id);
        if (skill && !skill.attributeId) {
          this.form.get('attribute').addValidators(Validators.required);
        } else {
          this.form.get('attribute').setValue( {
            type: PropertyType.Attribute,
            id: skill.attributeId,
          } as Property);
        }
      }
    });
  }

  public roll() {
    const rollInput = this.form.value as RollInput;
    rollInput.rolls = rollInput.rollsAsString ? rollInput.rollsAsString.map(a => Number(a)) : [];
    this.campaignsService.rollForCreature(this.campaign.id, this.scene.id, rollInput.creature.id, rollInput)
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
