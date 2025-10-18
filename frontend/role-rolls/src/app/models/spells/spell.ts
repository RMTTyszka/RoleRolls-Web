export interface SpellCircle {
  id: string;
  circle: number;
  title: string;
  inGameDescription: string;
  effectDescription: string;
  castingTime: string;
  duration: string;
  areaOfEffect: string;
  requirements: string;
  levelRequirement: number;
}

export interface Spell {
  id: string;
  name: string;
  description: string;
  mdDescription: string;
  circles: SpellCircle[];
}
