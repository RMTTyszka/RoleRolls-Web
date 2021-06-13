import {ModuleDataColor} from '../shared/ModuleDataColor';
import {
  DANGER_COLOR,
  INFO_COLOR,
  MONSTER_COLOR_1,
  MONSTER_COLOR_2,
  MONSTER_COLOR_3,
  MONSTER_COLOR_4,
  MONSTER_COLOR_5, PRIMARY_COLOR, SECONDARY_COLOR, SUCCESS_COLOR, WARNING_COLOR
} from '../theming/rr-colors';

export class MonstersColorConfig implements ModuleDataColor {
  color1 = MONSTER_COLOR_1;
  color2 = MONSTER_COLOR_2;
  color3 = MONSTER_COLOR_3;
  color4 = MONSTER_COLOR_4;
  color5 = MONSTER_COLOR_5;
  dangerColor = DANGER_COLOR;
  infoColor = INFO_COLOR;
  primaryColor = MONSTER_COLOR_4;
  secondaryColor = SECONDARY_COLOR;
  successColor = SUCCESS_COLOR;
  warningColor = WARNING_COLOR;
}
