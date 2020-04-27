import {EffectType} from './EffectType.model';
import {Entity} from '../Entity.model';

export class EffectInstance extends Entity {
    public effectType: EffectType;
    public level: number;
    public difficulty: number;
    public complexity: number;
    public duration: number;
}
