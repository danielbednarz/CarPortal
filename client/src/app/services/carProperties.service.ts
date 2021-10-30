import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Brand } from '../models/brand';
import { Engine } from '../models/engine';
import { EnginesForModel } from '../models/enginesForModel';
import { Model } from '../models/model';

@Injectable({
  providedIn: 'root'
})
export class CarPropertiesService {
  baseUrl = environment.baseApiUrl + 'carProperties';
  brands: Brand[]

  constructor(private http: HttpClient) { }

  getBrands() {
    return this.http.get<Brand[]>(this.baseUrl + '/getBrands');
  }

  getModels(id: number) {
    return this.http.get<Model[]>(this.baseUrl + '/getModels/' + id);
  }

  getEnginesForModel(modelId: number) {
    debugger;
    return this.http.get<EnginesForModel[]>(this.baseUrl + '/getEnginesForModel/' + modelId);
  }

}
