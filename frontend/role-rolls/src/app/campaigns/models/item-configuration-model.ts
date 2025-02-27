import { Property } from '@app/models/bonuses/bonus';

export class ItemConfigurationModel {
  public armorProperty?: Property | null;
  public basicAttackTargetFirstLife?: Property | null;
  public meleeLightWeaponHitProperty?: Property | null;
  public meleeMediumWeaponHitProperty?: Property | null;
  public meleeHeavyWeaponHitProperty?: Property | null;
  public meleeLightWeaponDamageProperty?: Property | null;
  public meleeMediumWeaponDamageProperty?: Property | null;
  public meleeHeavyWeaponDamageProperty?: Property | null;
  public rangedLightWeaponHitProperty?: Property | null;
  public rangedMediumWeaponHitProperty?: Property | null;
  public rangedHeavyWeaponHitProperty?: Property | null;
  public rangedLightWeaponDamageProperty?: Property | null;
  public rangedMediumWeaponDamageProperty?: Property | null;
  public rangedHeavyWeaponDamageProperty?: Property | null;
  public basicAttackTargetSecondLife?: Property | null;
}
