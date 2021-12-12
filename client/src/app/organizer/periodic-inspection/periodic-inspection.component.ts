import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { PeriodicInspection } from 'src/app/models/periodicInspection';
import { OrganizerService } from 'src/app/services/organizer.service';

@Component({
  selector: 'app-periodic-inspection',
  templateUrl: './periodic-inspection.component.html',
  styleUrls: ['./periodic-inspection.component.css']
})
export class PeriodicInspectionComponent implements OnInit {
  isPeriodicInspectionPopupVisible = false;
  periodicInspection: PeriodicInspection[];
  periodicInspectionFormPopup: FormGroup;
  periodicInspectionRemainingDays: number;

  constructor(private http: HttpClient, private toastr: ToastrService, 
    private organizerService: OrganizerService, private formBuilder: FormBuilder,
    private router: Router) { }

  ngOnInit(): void {
    this.initializePeriodicInspectionForm();
    this.getPeriodicInspections();
    this.getPeriodicInspectionsRemainingDays();
  }

  initializePeriodicInspectionForm() {
    this.periodicInspectionFormPopup = this.formBuilder.group({
      inspectionDate: ['', Validators.required],
      isPositive: false,
    });
  }

  getPeriodicInspections() {
    this.organizerService.getPeriodicInspections().subscribe(periodicInspection => {
      this.periodicInspection = periodicInspection;
    })
  }

  getPeriodicInspectionsRemainingDays() {
    this.organizerService.getPeriodicInspectionsRemainingDays().subscribe(periodicInspectionRemainingDays => {
      this.periodicInspectionRemainingDays = periodicInspectionRemainingDays;
    })
  }

  addPeriodicInspection() {
    this.organizerService.addPeriodicInspection(this.periodicInspectionFormPopup.value).subscribe(() => {
      this.periodicInspectionFormPopup.reset();
      this.toastr.success("Przegląd okresowy został dodany");
      this.getPeriodicInspectionsRemainingDays();
      this.reloadComponent();
    })
  }

  deletePeriodicInspection(e: any) {
    const periodicInspectionId = e.key.id;
    this.organizerService.deletePeriodicInspection(periodicInspectionId).subscribe(() => {
      this.toastr.success("Przegląd okresowy został usunięty");
      this.getPeriodicInspectionsRemainingDays();
      this.reloadComponent();
    })
  }

  turnOnPeriodicInspectionPopup() {
    this.isPeriodicInspectionPopupVisible = true;
  }

  reloadComponent() {
    let currentUrl = this.router.url;
    this.router.routeReuseStrategy.shouldReuseRoute = () => false;
    this.router.onSameUrlNavigation = 'reload';
    this.router.navigate([currentUrl]);
 }

}
