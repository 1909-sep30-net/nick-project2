import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import FridgeItem from './models/fridge-item';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class FridgeApiService {

  constructor(private httpClient: HttpClient) { }

  getItems(): Promise<FridgeItem[]> {
    let url = `${environment.fridgeApiBaseUrl}/api/fridgeitems`;
    return this.httpClient.get<FridgeItem[]>(url).toPromise();
  }
}
