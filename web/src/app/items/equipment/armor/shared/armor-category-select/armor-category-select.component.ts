import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {FormControl, FormGroup} from '@angular/forms';
import {ArmorCategory} from '../../../../../shared/models/items/ArmorCategory.model';
import {ArmorCategoryService} from '../../armor-category.service';

@Component({
  selector: 'loh-armor-category-select',
  templateUrl: './armor-category-select.component.html',
  styleUrls: ['./armor-category-select.component.css']
})
export class ArmorCategorySelectComponent implements OnInit {

  armorCategories: ArmorCategory[] = [];
  @Input() form: FormGroup;
  @Input() controlName = 'category';
  @Output() armorCategorySelected = new EventEmitter<ArmorCategory>();
  constructor(
    private service: ArmorCategoryService
  ) {
  }

  ngOnInit() {
    this.form.setControl('armorCategorySelection', new FormControl());
    this.service.getAll().subscribe(categories => {
      this.armorCategories = categories;
      const category = this.armorCategories.find(cat => cat === this.form.get(this.controlName).value)
        || this.armorCategories[0];
      this.form.get('armorCategorySelection').setValue(category);
      this.form.get(this.controlName).patchValue(category);
    });
  }

  onChange(armorCategory: ArmorCategory) {
    this.form.get(this.controlName).patchValue(armorCategory);
  }

}
