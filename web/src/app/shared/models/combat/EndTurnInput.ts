export class EndTurnInput {
  combatId: string;
  creatureId: string;


  constructor(combatId: string, creatureId: string) {
    this.combatId = combatId;
    this.creatureId = creatureId;
  }
}
