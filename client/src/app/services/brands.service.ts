import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Brand } from '../models/brand';

@Injectable({
  providedIn: 'root'
})
export class BrandsService {
  baseUrl = environment.baseApiUrl;

  constructor(private http: HttpClient) { }

  getBrands() {
    return this.http.get<Brand[]>(this.baseUrl + 'carProperties/getBrands');
  }

}
