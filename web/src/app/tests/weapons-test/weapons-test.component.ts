import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup} from '@angular/forms';
import {TestsService} from '../tests.service';
import {MessageService} from 'primeng/api';

export interface IDamage {
  damage: number;
  attacks: number;
  total: number;
  baseAttacks: number;
  weaps: number;
  hit: number;
}

export interface Ipvs {
  life: number;
  moral: number;
}

@Component({
  selector: 'loh-weapons-test',
  templateUrl: './weapons-test.component.html',
  styleUrls: ['./weapons-test.component.css']
})

export class WeaponsTestComponent implements OnInit {

  twoSmallWeapons: IDamage[];
  twoMediumWeapons: IDamage[];
  oneHeavyWeapon: IDamage[];
  oneSmallWeapon: IDamage[];
  oneMediumWeapon: IDamage[];
  onehandedHeavyWeapon: IDamage[];
  twohandedMediumWeapon: IDamage[];

  ARMORlight: number[];
  ARMORmedium: number[];
  ARMORheavy: number[];

  damagePerArmortwoSmallWeapons: number[];
  damagePerArmortwoMediumWeapons: number[];
  damagePerArmoroneHeavyWeapon: number[];
  damagePerArmoroneSmallWeapon: number[];
  damagePerArmoroneMediumWeapon: number[];
  damagePerArmoronehandedHeavyWeapon: number[];
  damagePerArmortwohandedMediumWeapon: number[];

  pvs: number[] = [];
  realPvs: Ipvs[] = [];
  form: FormGroup;

  isLoading = true;
  constructor(
    private fb: FormBuilder,
    private testService: TestsService,
    private messageService: MessageService
  ) { }

  makeWeaponTest() {
    this.testService.makeWeaponTest().subscribe(() => this.messageService.add({severity: 'success', detail: 'completed'}), (error: any) => this.messageService.add({severity: 'error', detail: error.message}));
    this.testService.test();
  }

  ngOnInit() {
    this.form = this.fb.group({
      twoSWDAM: this.fb.control(2),
      twoSWMBON: this.fb.control(2),
      twoSWMATTR: this.fb.control(2),
      twoSWATS: this.fb.control(1),
      twoSWHIT: this.fb.control(-1),

      twoMWDAM: this.fb.control(4),
      twoMWMBON: this.fb.control(3),
      twoMWMATTR: this.fb.control(2),
      twoMWATS: this.fb.control(2),
      twoMWHIT: this.fb.control(-1),

      oneHWDAM: this.fb.control(6),
      oneHWMBON: this.fb.control(4),
      oneHWMATTR: this.fb.control(4),
      oneHWATS: this.fb.control(3),
      oneHWHIT: this.fb.control(3),

      oneSWDAM: this.fb.control(2),
      oneSWMBON: this.fb.control(3),
      oneSWMATTR: this.fb.control(2),
      oneSWATS: this.fb.control(1),
      oneSWHIT: this.fb.control(0),

      oneMWDAM: this.fb.control(5),
      oneMWMBON: this.fb.control(3),
      oneMWMATTR: this.fb.control(3),
      oneMWATS: this.fb.control(2),
      oneMWHIT: this.fb.control(0),

      onehHWDAM: this.fb.control(5),
      onehHWMBON: this.fb.control(4),
      onehHWMATTR: this.fb.control(3),
      onehHWATS: this.fb.control(2),
      onehHWHIT: this.fb.control(-1),

      twohMWDAM: this.fb.control(5),
      twohMWMBON: this.fb.control(4),
      twohMWMATTR: this.fb.control(4),
      twohMWATS: this.fb.control(3),
      twohMWHIT: this.fb.control(2),

      defLA: this.fb.control(1),
      evLA: this.fb.control(1),
      baseLA: this.fb.control(1),

      defMA: this.fb.control(2),
      evMA: this.fb.control(0),
      baseMA: this.fb.control(3),

      defHA: this.fb.control(3),
      evHA: this.fb.control(-1),
      baseHA: this.fb.control(4),

    });
    this.isLoading = false;
    this.form.valueChanges.subscribe(data => {
      this.updateDamages();
      this.getdamagePerArmorDamages2();
      console.log(this.form.get('oneHWHIT').value);
    });
    this.updateDamages();

    this.getdamagePerArmorDamages2();

    this.calculatePv();

  }

  getDamage(damage, mb, mattr, attks: number, hit, lvl, weps = 1): IDamage {
    const dam = damage +
      this.getBonus(lvl) * mb +
      this.getAttr(lvl) * mattr;
    return <IDamage>{
      damage: dam,
      attacks: this.getAttks(lvl, attks, weps),
      total: dam * this.getAttks(lvl, attks, weps),
      baseAttacks: attks,
      weaps: weps,
      hit: hit
    };
  }

  getBonus(lvl): number {

    return Math.floor(lvl / 2);
  }
  getAttr(lvl): number {
    const attributeLevel = 4 + Math.floor((lvl + 1) / 5) + Math.floor((lvl + 4) / 5) - 1;
    return attributeLevel;
  }
  getAttks(lvl, attks: number, wep = 1) {


    return Math.ceil(Math.floor(this.getAttr(lvl) / 2) * (1 / attks) * wep);
  }
  getDefense(lvl, def, base) {
    return null;
  }

  updateDamages() {
    this.twoSmallWeapons = [];
    this.twoMediumWeapons = [];
    this.oneHeavyWeapon = [];
    this.oneSmallWeapon = [];
    this.oneMediumWeapon = [];
    this.onehandedHeavyWeapon = [];
    this.twohandedMediumWeapon = [];


    for (let x = 0; x < 20; x++) {
      this.twoSmallWeapons.push(this.getDamage(this.form.get('twoSWDAM').value,
        this.form.get('twoSWMBON').value,
        this.form.get('twoSWMATTR').value,
        this.form.get('twoSWATS').value,
        this.form.get('twoSWHIT').value,
        x + 1, 2));
      this.twoMediumWeapons.push(this.getDamage(this.form.get('twoMWDAM').value,
        this.form.get('twoMWMBON').value,
        this.form.get('twoMWMATTR').value,
        this.form.get('twoMWATS').value,
        this.form.get('twoMWHIT').value,
        x + 1, 2));
      this.oneHeavyWeapon.push(this.getDamage(this.form.get('oneHWDAM').value,
        this.form.get('oneHWMBON').value,
        this.form.get('oneHWMATTR').value,
        this.form.get('oneHWATS').value,
        this.form.get('oneHWHIT').value,
        x + 1, 1));
      this.oneSmallWeapon.push(this.getDamage(this.form.get('oneSWDAM').value,
        this.form.get('oneSWMBON').value,
        this.form.get('oneSWMATTR').value,
        this.form.get('oneSWATS').value,
        this.form.get('oneSWHIT').value,
        x + 1, 1));
      this.oneMediumWeapon.push(this.getDamage(this.form.get('oneMWDAM').value,
        this.form.get('oneMWMBON').value,
        this.form.get('oneMWMATTR').value,
        this.form.get('oneMWATS').value,
        this.form.get('oneMWHIT').value,
        x + 1, 1));
      this.onehandedHeavyWeapon.push(this.getDamage(this.form.get('onehHWDAM').value,
        this.form.get('onehHWMBON').value,
        this.form.get('onehHWMATTR').value,
        this.form.get('onehHWATS').value,
        this.form.get('onehHWHIT').value,
        x + 1, 1));
      this.twohandedMediumWeapon.push(this.getDamage(this.form.get('twohMWDAM').value,
        this.form.get('twohMWMBON').value,
        this.form.get('twohMWMATTR').value,
        this.form.get('twohMWATS').value,
        this.form.get('twohMWHIT').value,
        x + 1, 1));
    }
  }

  calculateDamageFordamagePerArmor(iDamage: IDamage[], def, base, evade, damagePerArmor: number[], armor: number[]) {
    iDamage.forEach((dam, index) => {
      const damage = dam.damage - def * this.getBonus(index + 1) - base - this.getAttr(index + 1);
      let totalDamage = 0;
      for (let count = 0; count < 1000; count++) {
        let hits = 0;
        for (let x = 0; x < (this.getAttr(index + 1) + this.getLevelBonus(index + 1)) * dam.weaps; x++) {
          const roll = Math.random() * 20;
          if (roll + dam.hit >= 10) {
            hits++;
          }
        }
        const totalHits = Math.floor(hits / dam.baseAttacks) - evade < 0 ? 0 : Math.floor(hits / dam.baseAttacks) - evade;
        totalDamage += damage * totalHits;
      }
      // console.log(totalDamage);
      damagePerArmor.push(Math.round(totalDamage / 1000));
      armor.push(Math.round(totalDamage / 1000));
    });
  }

  getLevelBonus(level: number) {
    return Math.floor(level / 10);
  }
  getdamagePerArmorDamages2() {
    this.damagePerArmortwoSmallWeapons = [];
    this.damagePerArmortwoMediumWeapons = [];
    this.damagePerArmoroneHeavyWeapon = [];
    this.damagePerArmoroneSmallWeapon = [];
    this.damagePerArmoroneMediumWeapon = [];
    this.damagePerArmoronehandedHeavyWeapon = [];
    this.damagePerArmortwohandedMediumWeapon = [];
    this.ARMORlight = [];
    this.ARMORmedium = [];
    this.ARMORheavy = [];
    this.calculateDamageFordamagePerArmor(this.twoSmallWeapons,
      this.form.get('defLA').value, this.form.controls.baseLA.value,
      this.form.controls.evLA.value,
      this.damagePerArmortwoSmallWeapons,
      this.ARMORlight);
    this.calculateDamageFordamagePerArmor(this.twoSmallWeapons,
      this.form.get('defMA').value, this.form.controls.baseMA.value,
      this.form.controls.evMA.value,
      this.damagePerArmortwoSmallWeapons,
      this.ARMORmedium);
    this.calculateDamageFordamagePerArmor(this.twoSmallWeapons,
      this.form.get('defHA').value, this.form.controls.baseHA.value,
      this.form.controls.evHA.value,
      this.damagePerArmortwoSmallWeapons,
      this.ARMORheavy);

    this.calculateDamageFordamagePerArmor(this.twoMediumWeapons,
      this.form.get('defLA').value, this.form.controls.baseLA.value,
      this.form.controls.evLA.value,
      this.damagePerArmortwoMediumWeapons,
      this.ARMORlight);
    this.calculateDamageFordamagePerArmor(this.twoMediumWeapons,
      this.form.get('defMA').value, this.form.controls.baseMA.value,
      this.form.controls.evMA.value,
      this.damagePerArmortwoMediumWeapons,
      this.ARMORmedium);
    this.calculateDamageFordamagePerArmor(this.twoMediumWeapons,
      this.form.get('defHA').value, this.form.controls.baseHA.value,
      this.form.controls.evHA.value,
      this.damagePerArmortwoMediumWeapons,
      this.ARMORheavy);

    this.calculateDamageFordamagePerArmor(this.oneHeavyWeapon,
      this.form.get('defLA').value, this.form.controls.baseLA.value,
      this.form.controls.evLA.value,
      this.damagePerArmoroneHeavyWeapon,
      this.ARMORlight);
    this.calculateDamageFordamagePerArmor(this.oneHeavyWeapon,
      this.form.get('defMA').value, this.form.controls.baseMA.value,
      this.form.controls.evMA.value,
      this.damagePerArmoroneHeavyWeapon,
      this.ARMORmedium);
    this.calculateDamageFordamagePerArmor(this.oneHeavyWeapon,
      this.form.get('defHA').value, this.form.controls.baseHA.value,
      this.form.controls.evHA.value,
      this.damagePerArmoroneHeavyWeapon,
      this.ARMORheavy);

    this.calculateDamageFordamagePerArmor(this.oneSmallWeapon,
      this.form.get('defLA').value, this.form.controls.baseLA.value,
      this.form.controls.evLA.value,
      this.damagePerArmoroneSmallWeapon,
      this.ARMORlight);
    this.calculateDamageFordamagePerArmor(this.oneSmallWeapon,
      this.form.get('defMA').value, this.form.controls.baseMA.value,
      this.form.controls.evMA.value,
      this.damagePerArmoroneSmallWeapon,
      this.ARMORmedium);
    this.calculateDamageFordamagePerArmor(this.oneSmallWeapon,
      this.form.get('defHA').value, this.form.controls.baseHA.value,
      this.form.controls.evHA.value,
      this.damagePerArmoroneSmallWeapon,
      this.ARMORheavy);

    this.calculateDamageFordamagePerArmor(this.oneMediumWeapon,
      this.form.get('defLA').value, this.form.controls.baseLA.value,
      this.form.controls.evLA.value,
      this.damagePerArmoroneMediumWeapon,
      this.ARMORlight);
    this.calculateDamageFordamagePerArmor(this.oneMediumWeapon,
      this.form.get('defMA').value, this.form.controls.baseMA.value,
      this.form.controls.evMA.value,
      this.damagePerArmoroneMediumWeapon,
      this.ARMORmedium);
    this.calculateDamageFordamagePerArmor(this.oneMediumWeapon,
      this.form.get('defHA').value, this.form.controls.baseHA.value,
      this.form.controls.evHA.value,
      this.damagePerArmoroneMediumWeapon,
      this.ARMORheavy);

    this.calculateDamageFordamagePerArmor(this.onehandedHeavyWeapon,
      this.form.get('defLA').value, this.form.controls.baseLA.value,
      this.form.controls.evLA.value,
      this.damagePerArmoronehandedHeavyWeapon,
      this.ARMORlight);
    this.calculateDamageFordamagePerArmor(this.onehandedHeavyWeapon,
      this.form.get('defMA').value, this.form.controls.baseMA.value,
      this.form.controls.evMA.value,
      this.damagePerArmoronehandedHeavyWeapon,
      this.ARMORmedium);
    this.calculateDamageFordamagePerArmor(this.onehandedHeavyWeapon,
      this.form.get('defHA').value, this.form.controls.baseHA.value,
      this.form.controls.evHA.value,
      this.damagePerArmoronehandedHeavyWeapon,
      this.ARMORheavy);

    this.calculateDamageFordamagePerArmor(this.twohandedMediumWeapon,
      this.form.get('defLA').value, this.form.controls.baseLA.value,
      this.form.controls.evLA.value,
      this.damagePerArmortwohandedMediumWeapon,
      this.ARMORlight);
    this.calculateDamageFordamagePerArmor(this.twohandedMediumWeapon,
      this.form.get('defMA').value, this.form.controls.baseMA.value,
      this.form.controls.evMA.value,
      this.damagePerArmortwohandedMediumWeapon,
      this.ARMORmedium);
    this.calculateDamageFordamagePerArmor(this.twohandedMediumWeapon,
      this.form.get('defHA').value, this.form.controls.baseHA.value,
      this.form.controls.evHA.value,
      this.damagePerArmortwohandedMediumWeapon,
      this.ARMORheavy);

  }

  total(a: any[]) {
    return a.reduce((x, y) => x + y, 0);
  }

  calculatePv() {
    this.pvs = [];
    for (let x = 0; x < 20; x++) {
      let pv = this.damagePerArmortwoSmallWeapons[x] +
        this.damagePerArmortwoMediumWeapons[x] +
        this.damagePerArmoroneHeavyWeapon[x] +
        this.damagePerArmoroneSmallWeapon[x] +
        this.damagePerArmoroneMediumWeapon[x] +
        this.damagePerArmoronehandedHeavyWeapon[x] +
        this.damagePerArmortwohandedMediumWeapon[x];
      pv += this.damagePerArmortwoSmallWeapons[x + 20] +
        this.damagePerArmortwoMediumWeapons[x + 20] +
        this.damagePerArmoroneHeavyWeapon[x + 20] +
        this.damagePerArmoroneSmallWeapon[x + 20] +
        this.damagePerArmoroneMediumWeapon[x + 20] +
        this.damagePerArmoronehandedHeavyWeapon[x + 20] +
        this.damagePerArmortwohandedMediumWeapon[x + 20];
      pv += this.damagePerArmortwoSmallWeapons[x + 40] +
        this.damagePerArmortwoMediumWeapons[x + 40] +
        this.damagePerArmoroneHeavyWeapon[x + 40] +
        this.damagePerArmoroneSmallWeapon[x + 40] +
        this.damagePerArmoroneMediumWeapon[x + 40] +
        this.damagePerArmoronehandedHeavyWeapon[x + 40] +
        this.damagePerArmortwohandedMediumWeapon[x + 40];
      pv /= 21;
      this.pvs.push(Math.round(pv * 4));
      this.realPvs.push(this.pv(x + 1));
    }
  }
  pv(level: number): Ipvs {
    const lifeBase = 4;
    const lifeMultBase = 2;
    const lifeMultLevel = 2;

    const moralBase = 4;
    const moralMultBase = 2;
    const moralMultLevel = 2;
    // const lifeBase = 6;
    // const lifeMultBase = 2;
    // const lifeMultLevel = 1;

    // const moralBase = 3;
    // const moralMultBase = 2;
    // const moralMultLevel = 1;


    const pv: Ipvs = {
      life: 5 + lifeBase  * level
        + (level + 2) * this.getAttr(level),
      moral: 5  + moralBase  * level
        + (level + 2) * this.getAttr(level) / 2,
    };
    // const pv: Ipvs = {
    //   life: level * lifeBase + lifeMultBase * this.getAttr(level)
    //     + level * this.getAttr(level) * lifeMultLevel,
    //   moral: moralBase * level + moralMultBase * this.getAttr(level)
    //     + level * this.getAttr(level) * moralMultLevel,
    // };
    return pv;
  }
}
