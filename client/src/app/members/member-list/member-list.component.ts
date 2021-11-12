import { HttpClient } from '@angular/common/http';
import { analyzeAndValidateNgModules } from '@angular/compiler';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, NgForm } from '@angular/forms';
import { Observable } from 'rxjs';
import { Brand } from 'src/app/models/brand';
import { Member } from 'src/app/models/member';
import { Model } from 'src/app/models/model';
import { Pagination } from 'src/app/models/paginaton';
import { UserParameters } from 'src/app/models/userParameters';
import { AccountService } from 'src/app/services/account.service';
import { CarPropertiesService } from 'src/app/services/carProperties.service';
import { MembersService } from 'src/app/services/members.service';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit {
  members: Member[];
  pagination: Pagination;
  userParams: UserParameters;
  brands: Brand[];
  models: Model[];
  isCollapsed = false;
  filterForm: FormGroup;

  constructor(private memberService: MembersService, private carPropertiesService: CarPropertiesService, private formBuilder: FormBuilder) { 
    this.userParams = new UserParameters();
  }

  ngOnInit(): void {
    this.initializeForm();
    this.loadMembers();
    this.loadBrands();
  }

  initializeForm() {
    this.filterForm = this.formBuilder.group({
      brand: '',
      model: '',
      minPower: '',
      maxPower: ''
    });
  }

  loadMembers() {
    this.memberService.getMembers(this.userParams).subscribe(response => {
      this.members = response.result;
      this.pagination = response.pagination;
    })
  }

  loadBrands() {
    this.carPropertiesService.getBrands().subscribe(brands => {
      this.brands = brands;
    })
  }

  loadModels(id: number) {
    this.carPropertiesService.getModels(id).subscribe(models => {
      this.models = models;
    })
  }

  resetFilters() {
    this.userParams = new UserParameters();
    this.loadMembers();
  }

  pageChanged(event: any) {
    this.userParams.pageNumber = event.page;
    this.loadMembers();
  }

  brandChanged(event: any) {
    this.userParams.brandId = parseInt(event.target['value']);
    this.filterForm.controls.model.setValue(0);
    this.loadMembers();

    const brandId: number = parseInt(event.target['value']);
    this.loadModels(brandId);
  }

  modelChanged(event: any) {
    this.userParams.modelId = parseInt(event.target['value']);
    this.loadMembers();
  }

}
