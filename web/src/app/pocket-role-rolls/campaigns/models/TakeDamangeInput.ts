import { PocketCreature } from '../../../shared/models/pocket/creatures/pocket-creature';
import {EquipableSlot} from '../../../shared/models/pocket/itens/equipable-slot';

export class TakeDamageInput {
    public creature: PocketCreature;
}
export class AttackInput {
    public slot: EquipableSlot;
    public defenseId: string;
    public lifeId: string;
    public targetId: string;
}
