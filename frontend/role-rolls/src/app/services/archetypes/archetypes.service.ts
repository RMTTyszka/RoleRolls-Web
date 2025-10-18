import { Injectable } from '@angular/core';
import {BaseCrudService} from '@services/base-service/base-crud-service';
import {HttpClient, HttpParams} from '@angular/common/http';
import {PagedOutput} from '@app/models/PagedOutput';
import {Observable} from 'rxjs';
import {RR_API} from '@app/tokens/loh.api';
import { Bonus } from '@app/models/bonuses/bonus';
import {Archetype, ArchetypePowerDescription} from '@app/models/archetypes/archetype';
import { Spell } from '@app/models/spells/spell';

@Injectable({
  providedIn: 'root'
})
export class ArchetypesService {
  public path(templateId: string): string {
    return `campaign-templates/${templateId}/archetypes`;
  };

  constructor(
    private http: HttpClient,
  ) {
  }
  public getById(templateId: string, archetypeId: string): Observable<Archetype> {
    return this.http.get<Archetype>(`${RR_API.backendUrl}${this.path(templateId)}/${archetypeId}`);
  }

  public getList(templateId: string, query: { [key: string]: any }): Observable<PagedOutput<Archetype>> {
    const params = Object.entries(query).reduce((httpParams, [key, value]) => {
      if (value !== undefined && value !== null) {
        return httpParams.set(key, value.toString());
      }
      return httpParams;
    }, new HttpParams());
    return this.http.get<PagedOutput<Archetype>>(`${RR_API.backendUrl}${this.path(templateId)}`, { params });
  }
  public addBonus(templateId: string, archetypeId: string, bonus: Bonus): Observable<Bonus> {
    return this.http.post<Bonus>(
      `${RR_API.backendUrl}${this.path(templateId)}/${archetypeId}/bonuses`,
      bonus
    );
  }

  public updateBonus(templateId: string, archetypeId: string, bonus: Bonus): Observable<Bonus> {
    return this.http.put<Bonus>(
      `${RR_API.backendUrl}${this.path(templateId)}/${archetypeId}/bonuses/${bonus.id}`,
      bonus
    );
  }

  public removeBonus(templateId: string, archetypeId: string, bonusId: string): Observable<void> {
    return this.http.delete<void>(
      `${RR_API.backendUrl}${this.path(templateId)}/${archetypeId}/bonuses/${bonusId}`
    );
  }
  public addPowerDescription(templateId: string, archetypeId: string, powerDescription: ArchetypePowerDescription): Observable<ArchetypePowerDescription> {
    return this.http.post<ArchetypePowerDescription>(
      `${RR_API.backendUrl}${this.path(templateId)}/${archetypeId}/power-descriptions`,
      powerDescription
    );
  }

  public updatePowerDescription(templateId: string, archetypeId: string, powerDescription: ArchetypePowerDescription): Observable<ArchetypePowerDescription> {
    return this.http.put<ArchetypePowerDescription>(
      `${RR_API.backendUrl}${this.path(templateId)}/${archetypeId}/power-descriptions/${powerDescription.id}`,
      powerDescription
    );
  }

  public removePowerDescription(templateId: string, archetypeId: string, powerDescriptionId: string): Observable<void> {
    return this.http.delete<void>(
      `${RR_API.backendUrl}${this.path(templateId)}/${archetypeId}/power-descriptions/${powerDescriptionId}`
    );
  }
  public addSpell(templateId: string, archetypeId: string, spell: Spell): Observable<Spell> {
    return this.http.post<Spell>(
      `${RR_API.backendUrl}${this.path(templateId)}/${archetypeId}/spells`,
      spell
    );
  }

  public updateSpell(templateId: string, archetypeId: string, spell: Spell): Observable<Spell> {
    return this.http.put<Spell>(
      `${RR_API.backendUrl}${this.path(templateId)}/${archetypeId}/spells/${spell.id}`,
      spell
    );
  }

  public removeSpell(templateId: string, archetypeId: string, spellId: string): Observable<void> {
    return this.http.delete<void>(
      `${RR_API.backendUrl}${this.path(templateId)}/${archetypeId}/spells/${spellId}`
    );
  }

}
