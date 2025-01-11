import { PocketCreature } from './pocket-creature';
import { EquipableSlot } from '../../models/equipable-slot';

export interface TakeDamageInput {
    creature: PocketCreature;
}
export interface AttackInput {
    slot: EquipableSlot;
    defenseId: string;
    hitPropertyId: string;
    damagePropertyId: string;
    lifeId: string;
    targetId: string;
}
