import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Brand } from '../models/brand';
import { Model } from '../models/model';
import { AccountService } from '../services/account.service';
import { CarPropertiesService } from '../services/carProperties.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();
  model: any = {};
  registerForm: FormGroup;
  brands: Brand[];
  models: Model[];

  constructor(private accountService: AccountService, private toastr: ToastrService,
    private formBuilder: FormBuilder, private carPropertiesService: CarPropertiesService) { }

  ngOnInit(): void {
    this.loadBrands();
    this.initializeForm();
  }

  initializeForm() {
    this.registerForm = this.formBuilder.group({
      username: ['', Validators.required],
      brand: ['', Validators.required],
      model: ['', Validators.required],
      engineCapacity: ['', Validators.required],
      enginePower: ['', Validators.required],
      mileage: ['', Validators.required],
      productionDate: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(8), Validators.maxLength(32)]],
      confirmPassword: ['', [Validators.required, this.matchValidator('password')]]
    });
    this.registerForm.controls.password.valueChanges.subscribe(() => {
      this.registerForm.controls.confirmPassword.updateValueAndValidity();
    })
  }

  matchValidator(matchingValue: string): ValidatorFn {
    return (control: AbstractControl) => {
      return control?.value === control?.parent?.controls[matchingValue].value ? null : {isMatching: true}
    };

  }

  register() {
    console.log(this.registerForm.value);
    // this.accountService.register(this.model).subscribe(response => {
    //   this.cancel();
    // }, error => {
    //   console.log(error);
    //   this.toastr.error(error.error);
    // })
  }

  cancel() {
    this.cancelRegister.emit(false);
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

  onChange(e) {
    this.registerForm.controls.model.setValue("");
    const brandName: number = parseInt(e.target['value']);
    this.loadModels(brandName);
  }

}
