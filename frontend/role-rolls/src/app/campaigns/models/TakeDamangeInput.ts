import { Creature } from 'app/campaigns/models/creature';
import { EquipableSlot } from 'app/models/itens/equipable-slot';

export interface TakeDamageInput {
    creature: Creature;
}
export interface AttackInput {
    slot: EquipableSlot;
    defenseId: string;
    hitPropertyId: string;
    damagePropertyId: string;
    lifeId: string;
    targetId: string;
}
