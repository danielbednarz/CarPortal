import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { FuelReport } from '../models/fuelReport';
import { FuelReportView } from '../models/fuelReportView';
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


}