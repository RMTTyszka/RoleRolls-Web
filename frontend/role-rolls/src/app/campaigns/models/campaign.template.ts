import { Entity } from '../../models/Entity.model';
import {Archetype} from '@app/models/archetypes/archetype';
import {CreatureType} from '@app/models/creatureTypes/creature-type';
import {ItemConfigurationModel} from '@app/campaigns/models/item-configuration-model';
import {
  FormulaToken
} from '@app/campaigns/models/formula-token.model';

export class CampaignTemplate implements Entity {
  public id: string;
  public name: string;
  public maxAttributePoints: number = 0;
  public totalAttributePoints: number = 0;
  public totalSkillsPoints: number = 0;
  public attributes: AttributeTemplate[] = [];
  public skills: SkillTemplate[] = [];
  // Removed back-compat getter; use skills.filter(s => !s.attributeId) where needed
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
  attributeTemplateId: string | null;
  skillTemplateId: string;
}

export interface VitalityTemplate extends Entity {
  name: string
  formula: string;
  formulaTokens: FormulaToken[];
}
export interface DefenseTemplate extends Entity {
  formula: string;
  formulaTokens: FormulaToken[];
  name: string;
}
