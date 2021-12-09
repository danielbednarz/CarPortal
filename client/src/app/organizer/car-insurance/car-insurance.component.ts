import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CarInsurance } from 'src/app/models/carInsurance';
import { enumType } from 'src/app/models/enumType';
import { OrganizerService } from 'src/app/services/organizer.service';

@Component({
  selector: 'app-car-insurance',
  templateUrl: './car-insurance.component.html',
  styleUrls: ['./car-insurance.component.css']
})
export class CarInsuranceComponent implements OnInit {
  isCarInsurancePopupVisible = false;
  carInsurance: CarInsurance[];
  carInsurancePopupForm: FormGroup;
  carInsuranceTypes: enumType[];
  carInsuranceRemainingDays: number;

  constructor(private organizerService: OrganizerService, private formBuilder: FormBuilder,
    private toastrService: ToastrService, private router: Router) { }

  ngOnInit(): void {
    this.getCarInsurances();
    this.getCarInsuranceTypes();
    this.getCarInsuranceRemainingDays();
    this.initializeCarInsurancePopupForm();
  }

  initializeCarInsurancePopupForm() {
    this.carInsurancePopupForm = this.formBuilder.group({
      carInsuranceType: ['', Validators.required],
      insurerName: ['', Validators.required],
      expirationDate: ['', Validators.required],
      cost: ['', Validators.required],
    });
  }

  getCarInsuranceTypes() {
    this.organizerService.getCarInsuranceTypes().subscribe(insuranceTypes => {
      this.carInsuranceTypes = insuranceTypes;
    });
  }

  getCarInsurances() {
    this.organizerService.getCarInsurance().subscribe(carInsurance => {
      this.carInsurance = carInsurance;
    })
  }

  getCarInsuranceRemainingDays() {
    this.organizerService.getCarInsuranceRemainingDays().subscribe(remainingDays => {
      this.carInsuranceRemainingDays = remainingDays;
    })
  }

  addCarInsurance() {
    this.organizerService.addCarInsurance(this.carInsurancePopupForm.value).subscribe(() => {
      this.carInsurancePopupForm.reset();
      this.toastrService.success("Ubezpieczenie auta zostało dodane");
      this.getCarInsuranceRemainingDays();
      this.reloadComponent();
    })
  }

  deleteCarInsurance(e) {
    const carInsuranceId = e.key.id;
    this.organizerService.deleteCarInsurance(carInsuranceId).subscribe(() => {
      this.toastrService.success("Ubezpieczenie auta zostało usunięte");
      this.getCarInsuranceRemainingDays();
      this.reloadComponent();
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
