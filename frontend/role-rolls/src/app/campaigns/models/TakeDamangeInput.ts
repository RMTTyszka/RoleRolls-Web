import { Creature } from 'app/campaigns/models/creature';
import { EquipableSlot } from 'app/models/itens/equipable-slot';
import { Property } from '@app/models/bonuses/bonus';

export interface TakeDamageInput {
    creature: Creature;
}
export interface AttackInput {
    slot: EquipableSlot;
    defense: string | null;
    hitProperty: Property | null;
    hitAttribute: Property | null;
    damageAttribute: Property | null;
    vitality: Property | null;
    targetId: string;
    luck: number;
    advantage: number;
}
