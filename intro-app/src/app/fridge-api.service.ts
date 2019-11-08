import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import FridgeItem from './models/fridge-item';
import { environment } from 'src/environments/environment';
import { AuthService } from './auth.service';
import User from './models/user';
import UserCreate from './models/user-create';
import FridgeItemCreate from './models/fridge-item-create';

@Injectable({
  providedIn: 'root'
})
export class FridgeApiService {
  user: User = null;

  constructor(
    private httpClient: HttpClient,
    auth: AuthService
  ) {
    auth.userProfile$.subscribe(user => {
      if (user) {
        this.getUser(user.email).catch((err: HttpErrorResponse) => {
          if (err.status === 404) {
            // if user does not exist, create
            return this.createUser({ email: user.email, name: user.name });
          } else {
            throw err;
          }
        }).then(apiUser => {
          this.user = apiUser;
        });
      }
    });
  }

  getItems(): Promise<FridgeItem[]> {
    const url = `${environment.fridgeApiBaseUrl}/api/fridgeitems`;
    return this.httpClient.get<FridgeItem[]>(url).toPromise();
  }

  addItem(item: FridgeItemCreate) {
    const url = `${environment.fridgeApiBaseUrl}/api/fridgeitems`;
    return this.httpClient.post<FridgeItem>(url, item).toPromise();
  }

  removeItem(id: number) {
    const url = `${environment.fridgeApiBaseUrl}/api/fridgeitems/${id}`;
    return this.httpClient.delete(url).toPromise();
  }

  cleanFridge() {
    const url = `${environment.fridgeApiBaseUrl}/api/fridgeitems/expired`;
    return this.httpClient.delete(url).toPromise();
  }

  getUser(email: string): Promise<User> {
    const url = `${environment.fridgeApiBaseUrl}/api/users/${email}`;
    return this.httpClient.get<User>(url).toPromise();
  }

  createUser(user: UserCreate): Promise<User> {
    const url = `${environment.fridgeApiBaseUrl}/api/users`;
    return this.httpClient.post<User>(url, user).toPromise();
  }
}
