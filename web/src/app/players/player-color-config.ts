import {ModuleDataColor} from '../shared/ModuleDataColor';
import {
  DANGER_COLOR,
  INFO_COLOR,
  PLAYER_COLOR_1,
  PLAYER_COLOR_2,
  PLAYER_COLOR_3,
  PLAYER_COLOR_4,
  PLAYER_COLOR_5, PRIMARY_COLOR, SECONDARY_COLOR, SUCCESS_COLOR, SUCCESS_HOVER_COLOR, WARNING_COLOR
} from '../theming/rr-colors';

export class PlayerColorConfig implements ModuleDataColor {
  color1 = PLAYER_COLOR_1;
  color2 = PLAYER_COLOR_2;
  color3 = PLAYER_COLOR_3;
  color4 = PLAYER_COLOR_4;
  color5 = PLAYER_COLOR_5;
  dangerColor = DANGER_COLOR;
  infoColor = INFO_COLOR;
  primaryColor = PRIMARY_COLOR;
  secondaryColor = SECONDARY_COLOR;
  successColor = SUCCESS_COLOR;
  successHoverColor = SUCCESS_HOVER_COLOR;
  warningColor = WARNING_COLOR;
}
