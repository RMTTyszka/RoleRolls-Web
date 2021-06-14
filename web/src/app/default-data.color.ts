import {ModuleDataColor} from './shared/ModuleDataColor';
import {
  DANGER_COLOR,
  INFO_COLOR,
  MAIN_COLOR_1,
  MAIN_COLOR_2,
  MAIN_COLOR_3,
  MAIN_COLOR_4,
  MAIN_COLOR_5, PRIMARY_COLOR, SECONDARY_COLOR, SUCCESS_COLOR, SUCCESS_HOVER_COLOR, WARNING_COLOR
} from './theming/rr-colors';

export class DefaultDataColor implements ModuleDataColor {
  color1 = MAIN_COLOR_1;
  color2 = MAIN_COLOR_2;
  color3 = MAIN_COLOR_3;
  color4 = MAIN_COLOR_4;
  color5 = MAIN_COLOR_5;
  dangerColor = DANGER_COLOR;
  infoColor = INFO_COLOR;
  primaryColor = PRIMARY_COLOR;
  secondaryColor = SECONDARY_COLOR;
  successColor = SUCCESS_COLOR;
  successHoverColor = SUCCESS_HOVER_COLOR;
  warningColor = WARNING_COLOR;
}
