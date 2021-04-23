import {Component, Input, OnInit} from '@angular/core';
import {EffectInstance} from '../../../shared/models/effects/EffectInstance.model';
import {EffectType} from '../../../shared/models/effects/EffectType.model';

@Component({
  selector: 'rr-effect-icon',
  templateUrl: './effect-icon.component.html',
  styleUrls: ['./effect-icon.component.css']
})
export class EffectIconComponent implements OnInit {

  @Input() effect: EffectInstance;
  constructor() { }

  ngOnInit() {
  }

  getIconClass() {
    switch (this.effect.effectType) {
      case EffectType.Death:
        return 'fas fa-skull death'
        break;
      case EffectType.Unconscious:
        return 'fas fa-skull'
        break;
      case EffectType.Poison:
        return 'fas fa-skull-crossbones'
        break;
      case EffectType.Burn:
        return 'fas fa-fire-alt'
        break;
      case EffectType.Slow:
        return 'fas fa-skull'
        break;

    }
  }
}
