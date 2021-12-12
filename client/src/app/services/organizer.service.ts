import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { CarInsurance } from '../models/carInsurance';
import { enumType } from '../models/enumType';
import { PeriodicInspection } from '../models/periodicInspection';

@Injectable({
  providedIn: 'root'
})
export class OrganizerService {
  baseUrl = environment.baseApiUrl + 'organizer';

  constructor(private http: HttpClient) { }

  getCarInsurance() {
    return this.http.get<CarInsurance[]>(this.baseUrl + '/getCarInsurance');
  }

  getCarInsuranceTypes() {
    return this.http.get<enumType[]>(this.baseUrl + '/getCarInsuranceTypes');
  }

  getCarInsuranceRemainingDays() {
    return this.http.get<number>(this.baseUrl + '/getCarInsuranceRemainingDays');
  }

  deleteCarInsurance(carInsuranceId: number) {
    return this.http.delete(this.baseUrl + '/deleteCarInsurance/' + carInsuranceId);
  }

  addCarInsurance(model: any) {
    return this.http.post<CarInsurance>(this.baseUrl + '/addCarInsurance', model);
  }

  getPeriodicInspections() {
    return this.http.get<PeriodicInspection[]>(this.baseUrl + '/getPeriodicInspections');
  }

  getPeriodicInspectionsRemainingDays() {
    return this.http.get<number>(this.baseUrl + '/getPeriodicInspectionRemainingDays');
  }

  addPeriodicInspection(model: any) {
    return this.http.post<PeriodicInspection>(this.baseUrl + '/addPeriodicInspection', model);
  }

  deletePeriodicInspection(periodicInspectionId: number) {
    return this.http.delete(this.baseUrl + '/deletePeriodicInspection/' + periodicInspectionId);
  }

}
