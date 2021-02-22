import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {LOH_API} from '../../loh.api';

@Injectable({
  providedIn: 'root'
})
export class CreatureEquipmentService {
  serverUrl = LOH_API.myBackUrl
  constructor(private httpClient: HttpClient) { }

  equipArmor(creatureId: string, armorId: string) {
    return this.httpClient.put(`${this.serverUrl}creatures/${creatureId}/equipment/armor`, {
      itemId: armorId
    });
  }
  equipMainWeapon(creatureId: string, weaponId: string) {
    return this.httpClient.put(`${this.serverUrl}creatures/${creatureId}/equipment/main-weapon`, {
      itemId: weaponId
    });
  }
  equipOffWeapon(creatureId: string, weaponId: string) {
    return this.httpClient.put(`${this.serverUrl}creatures/${creatureId}/equipment/off-weapon`, {
      itemId: weaponId
    });
  }
}
