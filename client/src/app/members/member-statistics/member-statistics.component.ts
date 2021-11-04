import { Component, Injectable, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs/operators';
import { FuelReport } from 'src/app/models/fuelReport';
import { FuelReportView } from 'src/app/models/fuelReportView';
import { Member } from 'src/app/models/member';
import { User } from 'src/app/models/user';
import { AccountService } from 'src/app/services/account.service';
import { MembersService } from 'src/app/services/members.service';
import { StatisticsService } from 'src/app/services/statistics.service';

@Component({
  selector: 'app-member-statistics',
  templateUrl: './member-statistics.component.html',
  styleUrls: ['./member-statistics.component.css']
})
export class MemberStatisticsComponent implements OnInit {
  dataSource: Data[];
  fuelReport: FuelReport[];
  fuelReportView: FuelReportView[];
  user: User;
  member: Member;
  popupVisible = false;
  fuelReportForm: FormGroup;
  validationErrors: string[] = [];

  constructor(private statisticsService: StatisticsService, private accountService: AccountService,
    private memberService: MembersService, private formBuilder: FormBuilder, private router: Router,
    private toastr: ToastrService) { 
    this.dataSource = data;
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
  }

  ngOnInit(): void {
    this.loadMember();
    this.initializeForm();
  }

  initializeForm() {
    this.fuelReportForm = this.formBuilder.group({
      cost: ['', Validators.required],
      fuelAmount: ['', Validators.required],
      traveledDistance: ['', Validators.required],
      refuelDate: ['', Validators.required],
      userId: ''
    })
  }

  loadFuelReport(userId: number) {
    this.statisticsService.getFuelReport(userId).subscribe(fuelReport => {
      this.fuelReport = fuelReport;
    })
  }

  loadFuelReportView(userId: number) {
    this.statisticsService.getFuelReportView(userId).subscribe(fuelReportView => {
      this.fuelReportView = fuelReportView;
    })
  }

  loadMember() {
    this.memberService.getMember(this.user.username).subscribe(member => {
      this.member = member;
      this.loadFuelReport(member.id);
      this.loadFuelReportView(member.id);
    })
  }

  addReport() {
    this.popupVisible = true;
  }

  onSubmit() {
    this.fuelReportForm.controls.userId.setValue(this.member.id);
    this.statisticsService.addNewFuelReport(this.fuelReportForm.value).subscribe(() => {
      this.fuelReportForm.reset();
      this.toastr.success('Wpis do raportu paliwowego został dodany');
      this.reloadComponent();
    }, error => {
      this.validationErrors = error;
    });
  }

  reloadComponent() {
    let currentUrl = this.router.url;
    this.router.routeReuseStrategy.shouldReuseRoute = () => false;
    this.router.onSameUrlNavigation = 'reload';
    this.router.navigate([currentUrl]);
 }

}

export class Data {
  day: string;
  oranges: number;
}

let data: Data[] = [{
  day: "Monday",
  oranges: 3
}, {
  day: "Tuesday",
  oranges: 2
}, {
  day: "Wednesday",
  oranges: 3
}, {
  day: "Thursday",
  oranges: 4
}, {
  day: "Friday",
  oranges: 6
}, {
  day: "Saturday",
  oranges: 11
}, {
  day: "Sunday",
  oranges: 4
}];

    
