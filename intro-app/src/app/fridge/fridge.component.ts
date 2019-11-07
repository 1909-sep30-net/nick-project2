import { Component, OnInit } from '@angular/core';
import FridgeItem from '../models/fridge-item';
import { FridgeApiService } from '../fridge-api.service';

@Component({
  selector: 'app-fridge',
  templateUrl: './fridge.component.html',
  styleUrls: ['./fridge.component.css']
})
export class FridgeComponent implements OnInit {
  private open: boolean = false;
  private items: FridgeItem[] = null;

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
      this.fridgeApi.getItems()
        .then(items => this.items = items);
    }
  }

  // if ctor param has access modifier,
  // TS will put that value into a same-name property of the class
  constructor(private fridgeApi: FridgeApiService) {
  }

  ngOnInit() {
  }

}
