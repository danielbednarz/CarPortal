import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CarInsurance } from '../models/carInsurance';
import { enumType } from '../models/enumType';
import { OrganizerService } from '../services/organizer.service';

@Component({
  selector: 'app-organizer',
  templateUrl: './organizer.component.html',
  styleUrls: ['./organizer.component.css']
})
export class OrganizerComponent implements OnInit {
  isCarInsurancePopupVisible = false;
  carInsurance: CarInsurance[];
  carInsurancePopupForm: FormGroup;
  carInsuranceTypes: enumType[];
  carInsuranceRemainingDays: number;

  constructor(private organizerService: OrganizerService, private formBuilder: FormBuilder,
    private toastrService: ToastrService, private router: Router) { }

  ngOnInit(): void {
    this.getCarInsuranceRemainingDays();
  }

  getCarInsuranceRemainingDays() {
    this.organizerService.getCarInsuranceRemainingDays().subscribe(remainingDays => {
      this.carInsuranceRemainingDays = remainingDays;
    })
  }

  turnOnCarInsurancePopup() {
    this.isCarInsurancePopupVisible = true;
  }

  reloadComponent() {
    let currentUrl = this.router.url;
    this.router.routeReuseStrategy.shouldReuseRoute = () => false;
    this.router.onSameUrlNavigation = 'reload';
    this.router.navigate([currentUrl]);
 }

}
