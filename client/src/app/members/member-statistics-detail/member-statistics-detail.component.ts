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
import { User } from 'src/app/models/user';
import { AccountService } from 'src/app/services/account.service';
import { MembersService } from 'src/app/services/members.service';
import { StatisticsService } from 'src/app/services/statistics.service';

@Component({
  selector: 'app-member-statistics-detail',
  templateUrl: './member-statistics-detail.component.html',
  styleUrls: ['./member-statistics-detail.component.css']
})
export class MemberStatisticsDetailComponent implements OnInit {
  fuelReport: FuelReport[];
  fuelReportView: FuelReportView[];
  member: Member;
  username: string;
  
  repairReport: RepairReport[];
  repairReportView: RepairReportView[];
  repairReportForm: FormGroup;
  repairReportPopupVisible = false;

  totalCostsReportView: TotalCostsReportView[];

  constructor(private route: ActivatedRoute, private statisticsService: StatisticsService, private memberService: MembersService) { }

  ngOnInit(): void {
    this.username = this.route.snapshot.paramMap.get('username');
    this.loadMember();
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

  loadTotalCostsReportView(userId: number) {
    this.statisticsService.getTotalCostsReportView(userId).subscribe(totalCostsReportView => {
      this.totalCostsReportView = totalCostsReportView;
    })
  }

  customPieChartLabel(arg) {
    return `${arg.valueText} zł (${arg.percentText} wydatków)`;
  }

  loadMember() {
    this.memberService.getMember(this.username).subscribe(member => {
      this.member = member;
      this.loadFuelReport(member.id);
      this.loadFuelReportView(member.id);
      this.loadRepairReport(member.id);
      this.loadRepairReportView(member.id);
      this.loadTotalCostsReportView(member.id);
    })
  }

}
