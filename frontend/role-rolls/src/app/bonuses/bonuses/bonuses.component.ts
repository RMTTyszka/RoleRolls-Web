import { Component, effect, EventEmitter, input, Output, signal, WritableSignal } from '@angular/core';
import { TableModule } from 'primeng/table';
import { Bonus, BonusType, BonusValueType, Property } from '@app/models/bonuses/bonus';
import { FormsModule } from '@angular/forms';
import { DropdownModule } from 'primeng/dropdown';
import { InputText } from 'primeng/inputtext';
import { ButtonDirective } from 'primeng/button';
import { Toolbar } from 'primeng/toolbar';
import { EditorAction, EntityActionData } from '@app/models/EntityActionData';
import { NgIf } from '@angular/common';

@Component({
  selector: 'rr-bonuses',
  imports: [
    TableModule,
    FormsModule,
    DropdownModule,
    InputText,
    ButtonDirective,
    Toolbar,
    NgIf
  ],
  templateUrl: './bonuses.component.html',
  styleUrl: './bonuses.component.scss',
  host: {
    'class': 'flex-grow-1 flex flex-col'
  }
})
export class BonusesComponent {
  @Output() bonusUpdated = new EventEmitter<EntityActionData<Bonus>>();
  public bonuses = input<Bonus[]>();
  public bonusesSignal: WritableSignal<Bonus[]> = signal(this.bonuses() || []);
  clonedBonuss: { [s: string]: Bonus } = {};
  constructor() {
    effect(() => {
      this.bonusesSignal.set(this.bonuses() || []);
    });
  }

  public valueTypes = [
    { label: 'Dices', value: BonusValueType.Dices },
    { label: 'Roll', value: BonusValueType.Roll }
  ];

  public bonusTypes = [
    { label: 'Innate', value: BonusType.Innate }
  ];

  public valueTypeLabel(type: BonusValueType): string {
    return this.valueTypes.find(vt => vt.value === type)?.label || '';
  }

  public bonusTypeLabel(type: BonusType): string {
    return this.bonusTypes.find(bt => bt.value === type)?.label || '';
  }
  saveBonus(bonus: Bonus) {
    console.log('Salvando bônus:', bonus);
    if (bonus.id) {
      this.bonusUpdated.emit({
        entity: bonus,
        action: EditorAction.update
      } as EntityActionData<Bonus>);
    } else {
      bonus.id = crypto.randomUUID();
      this.bonusUpdated.emit({
        entity: bonus,
        action: EditorAction.create
      } as EntityActionData<Bonus>);
    }
  }
  public addEmptyBonus() {
    const newBonus: Bonus = {
      id: null,
      name: 'new bonus',
      description: 'Description',
      value: 0,
      valueType: BonusValueType.Roll,
      property: {} as Property,
      type: BonusType.Innate
    };

    this.addBonus(newBonus);
  }
  onRowEditInit(bonus: Bonus) {
    this.clonedBonuss[bonus.id as string] = { ...bonus };
  }

  onRowEditSave(bonus: Bonus) {
      delete this.clonedBonuss[bonus.id as string];
      this.saveBonus(bonus);
  }

  onRowEditCancel(bonus: Bonus, index: number) {
    this.bonusesSignal()[index] = this.clonedBonuss[bonus.id as string];
    delete this.clonedBonuss[bonus.id as string];
  }
  public deleteBonus(bonus: Bonus) {
    this.bonusesSignal.update((bonuses: Bonus[]) => bonuses.filter(b => b.id !== bonus.id));
    this.bonusUpdated.emit({
      entity: bonus,
      action: EditorAction.delete
    } as EntityActionData<Bonus>);
  }

  public addBonus(newBonus: Bonus) {
    this.bonusesSignal.update((bonuses: Bonus[]) => [...bonuses, newBonus]);
  }
}
