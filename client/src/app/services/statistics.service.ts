import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { FuelReport } from '../models/fuelReport';
import { FuelReportView } from '../models/fuelReportView';
import { RepairReport } from '../models/repairReport';
import { RepairReportView } from '../models/repairReportView';
import { TotalCostsReportView } from '../models/totalCostsReportView';
import { TotalRepairFuelCostsReportView } from '../models/totalRepairFuelCostsReportView';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class StatisticsService {
  baseUrl = environment.baseApiUrl + 'statistics';
  private currentUserSource = new ReplaySubject<User>(1);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient) { }

  getFuelReport(userId: number) {
    return this.http.get<FuelReport[]>(this.baseUrl + '/getFuelReport/' + userId);
  }

  getFuelReportView(userId: number) {
    return this.http.get<FuelReportView[]>(this.baseUrl + '/getFuelReportView/' + userId);
  }

  deleteFuelReport(fuelReportId: string) {
    return this.http.delete(this.baseUrl + '/deleteFuelReport/' + fuelReportId);
  }

  getAverageConsumption(userId: number) {
    return this.http.get<FuelReportView[]>(this.baseUrl + '/getAverageConsumption/' + userId);
  }

  addNewFuelReport(model: any) {
    return this.http.post(this.baseUrl + '/add-fuel-report', model).pipe(
      map((user: User) => {
        if(user) {
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUserSource.next(user);
        }
      })
    );
  }

  getRepairReport(userId: number) {
    return this.http.get<RepairReport[]>(this.baseUrl + '/getRepairReport/' + userId);
  }

  getRepairReportView(userId: number) {
    return this.http.get<RepairReportView[]>(this.baseUrl + '/getRepairReportView/' + userId);
  }

  addRepairReport(model: any) {
    return this.http.post(this.baseUrl + '/addRepairReport', model).pipe(
      map((user: User) => {
        if(user) {
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUserSource.next(user);
        }
      })
    );
  }

  deleteRepairReport(repairReportId: string) {
    return this.http.delete(this.baseUrl + '/deleteRepairReport/' + repairReportId);
  }

  getTotalCostsReportView(userId: number) {
    return this.http.get<TotalCostsReportView[]>(this.baseUrl + '/getTotalCostsReportView/' + userId);
  }

  getTotalRepairFuelCostsReportView(userId: number) {
    return this.http.get<TotalRepairFuelCostsReportView[]>(this.baseUrl + '/getTotalRepairFuelCostsReportView/' + userId);
  }

}
