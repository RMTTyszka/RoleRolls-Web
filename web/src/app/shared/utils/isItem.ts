import { Entity } from '../models/Entity.model';
import { ArmorInstance } from '../models/ArmorInstance.model';
import { ItemInstance } from '../models/ItemInstance.model';

const isArmor = (armor: ItemInstance) => (armor as ArmorInstance).armorModel;
export default isArmor;
