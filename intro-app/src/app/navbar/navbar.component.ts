import { Component, OnInit } from '@angular/core';
import { AuthService } from '../auth.service';
import { FridgeApiService } from '../fridge-api.service';

// copied from Auth0 Angular quickstart.

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  constructor(
    public auth: AuthService,
    private fridgeApi: FridgeApiService
  ) { }

  get user() {
    return this.fridgeApi.user;
  }

  ngOnInit() {
  }

}
