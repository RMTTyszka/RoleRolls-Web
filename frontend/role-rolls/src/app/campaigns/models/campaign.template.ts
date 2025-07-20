import { Entity } from '../../models/Entity.model';
import {Archetype} from '@app/models/archetypes/archetype';
import {CreatureType} from '@app/models/creatureTypes/creature-type';
import {ItemConfigurationModel} from '@app/campaigns/models/item-configuration-model';

export class CampaignTemplate implements Entity {
  public id: string;
  public maxAttributePoints: number = 0;
  public totalAttributePoints: number = 0;
  public totalSkillsPoints: number = 0;
  public attributes: AttributeTemplate[] = [];
  public skills: SkillTemplate[] = [];
  public attributelessSkills: SkillTemplate[] = [];
  public vitalities: VitalityTemplate[] = [];
  public defenses: DefenseTemplate[] = [];
  public archetypes: Archetype[] = [];
  public creatureTypes: CreatureType[] = [];
  public default: boolean = false;
  itemConfiguration: ItemConfigurationModel;
  creatureTypeTitle: string;
  archetypeTitle: string;
}


export interface AttributeTemplate extends Entity {
  name: string
}

export interface SkillTemplate extends Entity {
  attributeId: string | null;
  name: string
  specificSkillTemplates: SpecificSkillsTemplate[];
}

export interface SpecificSkillsTemplate extends Entity {
  name: string
  attributeId: string | null;
  skillTemplateId: string;
}

export interface VitalityTemplate extends Entity {
  name: string
  formula: string;
}
export interface DefenseTemplate extends Entity {
  formula: string;
  name: string;
}
