import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {WeaponCategory} from 'src/app/shared/models/WeaponCategory.model';
import {FormControl, FormGroup} from '@angular/forms';
import {WeaponCategoryService} from '../weapon-category.service';

@Component({
  selector: 'rr-weapon-category-select',
  templateUrl: './weapon-category-select.component.html',
  styleUrls: ['./weapon-category-select.component.css']
})
export class WeaponCategorySelectComponent implements OnInit {

  weaponCategories: WeaponCategory[] = [];
  @Input() form: FormGroup;
  @Input() controlName = 'category';
  @Output() weaponCategorySelected = new EventEmitter<WeaponCategory>();
  constructor(
    private service: WeaponCategoryService
  ) {
  }

  ngOnInit() {
    this.form.setControl('weaponCategorySelection', new FormControl());
    this.service.getAll().subscribe(categories => {
      this.weaponCategories = categories.filter(cat => cat !== WeaponCategory.None);
      const category = this.weaponCategories.find(cat => cat === this.form.get(this.controlName ).value)
        || this.weaponCategories[0];
      this.form.get('weaponCategorySelection').setValue(category);
      this.form.get(this.controlName).patchValue(category);
    });
  }

  onChange(weaponCategory: WeaponCategory) {
    this.form.get(this.controlName).patchValue(weaponCategory);
  }

}
