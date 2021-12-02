import { Component, Injectable, Input, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs/operators';
import { FuelReport } from 'src/app/models/fuelReport';
import { FuelReportView } from 'src/app/models/fuelReportView';
import { Member } from 'src/app/models/member';
import { RepairReport } from 'src/app/models/repairReport';
import { RepairReportView } from 'src/app/models/repairReportView';
import { TotalCostsReportView } from 'src/app/models/totalCostsReportView';
import { TotalRepairFuelCostsReportView } from 'src/app/models/totalRepairFuelCostsReportView';
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
  member: Member;
  user: User;
  validationErrors: string[] = [];
  
  fuelReport: FuelReport[];
  fuelReportId: string;
  fuelReportView: FuelReportView[];
  fuelReportForm: FormGroup;
  fuelReportPopupVisible = false;

  repairReport: RepairReport[];
  repairReportId: string;
  repairReportView: RepairReportView[];
  repairReportForm: FormGroup;
  repairReportPopupVisible = false;

  totalCostsReportView: TotalCostsReportView[];
  totalRepairFuelCostsReportView: TotalRepairFuelCostsReportView[];

  constructor(private statisticsService: StatisticsService, public accountService: AccountService,
    private memberService: MembersService, private formBuilder: FormBuilder, private router: Router,
    private toastr: ToastrService) { 
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
  }

  ngOnInit(): void {
    this.loadMember();
    this.initializeFuelReportForm();
    this.initializeRepairReportForm();
  }

  initializeFuelReportForm() {
    this.fuelReportForm = this.formBuilder.group({
      cost: ['', Validators.required],
      fuelAmount: ['', Validators.required],
      traveledDistance: ['', Validators.required],
      refuelDate: ['', Validators.required],
      userId: ''
    })
  }

  initializeRepairReportForm() {
    this.repairReportForm = this.formBuilder.group({
      description: ['', Validators.required],
      cost: ['', Validators.required],
      repairDate: ['', Validators.required],
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

  deleteFuelReport(e: any) {
    this.fuelReportId = e.key;
    this.statisticsService.deleteFuelReport(this.fuelReportId).subscribe(() => {
      this.toastr.success("Wpis z raportu paliwowego został usunięty");
      this.reloadComponent();
    });
  }

  addFuelReportPopup() {
    this.fuelReportPopupVisible = true;
  }

  addRepairReportPopup() {
    this.repairReportPopupVisible = true;
  }

  onFuelReportSubmit() {
    this.fuelReportForm.controls.userId.setValue(this.member.id);
    this.statisticsService.addNewFuelReport(this.fuelReportForm.value).subscribe(() => {
      this.fuelReportForm.reset();
      this.toastr.success('Wpis do raportu paliwowego został dodany');
      this.reloadComponent();
    }, error => {
      this.validationErrors = error;
    });
  }

  onRepairReportSubmit() {
    this.repairReportForm.controls.userId.setValue(this.member.id);
    this.statisticsService.addRepairReport(this.repairReportForm.value).subscribe(() => {
      this.repairReportForm.reset();
      this.toastr.success('Wpis do raportu napraw został dodany');
      this.reloadComponent();
    }, error => {
      this.validationErrors = error;
    });
  }

  loadRepairReport(userId: number) {
    this.statisticsService.getRepairReport(userId).subscribe(repairReport => {
      this.repairReport = repairReport;
    })
  }

  loadRepairReportView(userId: number) {
    this.statisticsService.getRepairReportView(userId).subscribe(repairReportView => {
      this.repairReportView = repairReportView;
    })
  }

  deleteRepairReport(e: any) {
    this.repairReportId = e.key;
    this.statisticsService.deleteRepairReport(this.repairReportId).subscribe(() => {
      this.toastr.success("Wpis z raportu napraw i kosztów eksploatacyjnych został usunięty");
      this.reloadComponent();
    });
  }

  loadTotalCostsReportView(userId: number) {
    this.statisticsService.getTotalCostsReportView(userId).subscribe(totalCostsReportView => {
      this.totalCostsReportView = totalCostsReportView;
    })
  }

  loadTotalRepairFuelCostsReportView(userId: number) {
    this.statisticsService.getTotalRepairFuelCostsReportView(userId).subscribe(totalRepairFuelCostsReportView => {
      this.totalRepairFuelCostsReportView = totalRepairFuelCostsReportView;
    })
  }

  customPieChartLabel(arg) {
    return `${arg.valueText} zł (${arg.percentText} wydatków)`;
  }

  loadMember() {
    this.memberService.getMember(this.user.username).subscribe(member => {
      this.member = member;
      this.loadFuelReport(member.id);
      this.loadFuelReportView(member.id);
      this.loadRepairReport(member.id);
      this.loadRepairReportView(member.id);
      this.loadTotalCostsReportView(member.id);
      this.loadTotalRepairFuelCostsReportView(member.id);
    })
  }

  reloadComponent() {
    let currentUrl = this.router.url;
    this.router.routeReuseStrategy.shouldReuseRoute = () => false;
    this.router.onSameUrlNavigation = 'reload';
    this.router.navigate([currentUrl]);
 }

}


    
