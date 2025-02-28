import { Creature } from 'app/campaigns/models/creature';
import { EquipableSlot } from 'app/models/itens/equipable-slot';
import { Property } from '@app/models/bonuses/bonus';

export interface TakeDamageInput {
    creature: Creature;
}
export interface AttackInput {
    slot: EquipableSlot;
    defenseId: Property;
    hitPropertyId: Property;
    damagePropertyId: Property;
    lifeId: Property;
    targetId: string;
}
