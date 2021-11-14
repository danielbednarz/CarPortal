import { Component, Injectable, Input, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
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
  fuelReport: FuelReport[];
  fuelReportView: FuelReportView[];
  member: Member;
  user: User;
  popupVisible = false;
  fuelReportForm: FormGroup;
  validationErrors: string[] = [];

  constructor(private statisticsService: StatisticsService, public accountService: AccountService,
    private memberService: MembersService, private formBuilder: FormBuilder, private router: Router,
    private toastr: ToastrService) { 
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


    
