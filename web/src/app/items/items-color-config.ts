import {ModuleDataColor} from '../shared/ModuleDataColor';
import {
  DANGER_COLOR,
  INFO_COLOR,
  ITEM_COLOR_1,
  ITEM_COLOR_2,
  ITEM_COLOR_3,
  ITEM_COLOR_4,
  ITEM_COLOR_5, PRIMARY_COLOR, SECONDARY_COLOR, SUCCESS_COLOR, SUCCESS_HOVER_COLOR, WARNING_COLOR
} from '../theming/rr-colors';

export class ItemsColorConfig implements ModuleDataColor {
  color1 = ITEM_COLOR_1;
  color2 = ITEM_COLOR_2;
  color3 = ITEM_COLOR_3;
  color4 = ITEM_COLOR_4;
  color5 = ITEM_COLOR_5;
  dangerColor = DANGER_COLOR;
  infoColor = INFO_COLOR;
  primaryColor = ITEM_COLOR_4;
  secondaryColor = SECONDARY_COLOR;
  successColor = SUCCESS_COLOR;
  successHoverColor = SUCCESS_HOVER_COLOR;
  warningColor = WARNING_COLOR;
}
