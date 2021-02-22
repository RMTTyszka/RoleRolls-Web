import {Injectable, Injector} from '@angular/core';
import {ArmorCategory} from '../../../shared/models/items/ArmorCategory.model';
import {of} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ArmorCategoryService {
  constructor(
  ) {
  }

  public getAll() {
    return of<ArmorCategory[]>(<ArmorCategory[]> [
      ArmorCategory.None, ArmorCategory.Light, ArmorCategory.Medium, ArmorCategory.Heavy
    ]);
  }
}
