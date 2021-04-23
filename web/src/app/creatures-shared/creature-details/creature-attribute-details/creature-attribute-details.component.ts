import {Component, Input, OnInit} from '@angular/core';
import {Creature} from '../../../shared/models/creatures/Creature.model';
import {RollDifficulty, RollsCardComponent} from '../../rolls-card/rolls-card.component';
import {CreatureRollsService} from '../../creature-rolls.service';
import {MessageService} from 'primeng/api';
import {DialogService} from 'primeng/dynamicdialog';

@Component({
  selector: 'rr-creature-attribute-details',
  templateUrl: './creature-attribute-details.component.html',
  styleUrls: ['./creature-attribute-details.component.css']
})
export class CreatureAttributeDetailsComponent implements OnInit {
  @Input() public creature: Creature;
  public rollChanceToastKey = 'rollChanceToastKey';

  constructor(
    private readonly creatureRollsService: CreatureRollsService,
    private readonly messageService: MessageService,
    public dialogService: DialogService
  ) { }

  ngOnInit(): void {
  }


  roll(property: string, difficulty: number = null, complexity: number = null) {
    this.creatureRollsService.roll(this.creature.id, property, difficulty, complexity).subscribe((result) => {
      this.creatureRollsService.emitCreatureRolled(result);
    });
    return false;
  }
  openDetailedRoll(property: string) {
    this.dialogService.open(RollsCardComponent, {header: 'Roll', closeOnEscape: false, width: '50%'}).onClose.subscribe((difficulty: RollDifficulty) => {
      if (difficulty) {
        if (difficulty.shouldGetChance) {
          this.creatureRollsService.getChances(this.creature.id, property, difficulty.requiredChance).subscribe((result) => {
            this.messageService.add({
              key: this.rollChanceToastKey,
              severity: 'info',
              life: 100000,
              closable: true,
              data: result
            });
          });
        } else {
          this.roll(property, difficulty.difficulty, difficulty.complexity);
        }
      }
    });
    return false;
  }
}
