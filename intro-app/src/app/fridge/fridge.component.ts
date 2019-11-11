import { Component, OnInit } from '@angular/core';
import FridgeItem from '../models/fridge-item';
import { FridgeApiService } from '../fridge-api.service';
import { FormBuilder, Validators } from '@angular/forms';
import FridgeItemCreate from '../models/fridge-item-create';

@Component({
  selector: 'app-fridge',
  templateUrl: './fridge.component.html',
  styleUrls: ['./fridge.component.css']
})
export class FridgeComponent implements OnInit {
  open = false;
  items: FridgeItem[] | null = null;
  addItem = this.formBuilder.group({
    itemName: ['', Validators.required]
  });

  get user() {
    return this.fridgeApi.user;
  }

  // this is like a C# getter-only property
  get imageUrl() {
    if (this.open) {
      return 'assets/fridge-open.jpg';
    } else {
      return 'assets/fridge.jpg';
    }
  }

  toggleFridge(): void {
    if (this.open) {
      this.open = false;
    } else {
      this.open = true;
      // this.items = [
      //   { id: 1, name: 'coffee', expiration: new Date(2020, 3, 1) }
      // ];
      this.loadItems().then();
    }
  }

  loadItems() {
    return this.fridgeApi.getItems()
      .then(items => this.items = items);
  }

  cleanFridge() {
    this.fridgeApi.cleanFridge()
      .then(() => this.loadItems());
  }

  onSubmitAddItem() {
    const control = this.addItem.get('itemName');
    if (control) {
      const name = control.value as string;
      const item: FridgeItemCreate = { name };
      this.fridgeApi.addItem(item)
        .then(() => this.loadItems());
    }
  }

  remove(item: FridgeItem) {
    this.fridgeApi.removeItem(item.id)
      .then(() => this.loadItems());
  }

  // if ctor param has access modifier,
  // TS will put that value into a same-name property of the class
  constructor(
    private fridgeApi: FridgeApiService,
    private formBuilder: FormBuilder
  ) { }

  ngOnInit() {
  }

}
