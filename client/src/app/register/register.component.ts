import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormControl,
  FormGroup,
  ValidatorFn,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { environment } from 'src/environments/environment';
import { Brand } from '../models/brand';
import { Engine } from '../models/engine';
import { EnginesForModel } from '../models/enginesForModel';
import { Model } from '../models/model';
import { AccountService } from '../services/account.service';
import { CarPropertiesService } from '../services/carProperties.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();
  registerForm: FormGroup;
  brands: Brand[];
  models: Model[];
  engines: Engine[];
  validationErrors: string[] = [];
  siteKey = environment.siteKey;
  isRegisterSuccessInfoPopupVisible = false;

  constructor(
    private accountService: AccountService,
    private toastr: ToastrService,
    private formBuilder: FormBuilder,
    private carPropertiesService: CarPropertiesService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadBrands();
    this.initializeForm();
  }

  initializeForm() {
    this.registerForm = this.formBuilder.group({
      username: ['', Validators.required],
      brandId: ['', Validators.required],
      modelId: ['', Validators.required],
      engineId: ['', Validators.required],
      enginePower: ['', Validators.required],
      mileage: ['', Validators.required],
      productionDate: ['', Validators.required],
      password: [
        '',
        [
          Validators.required,
          Validators.minLength(8),
          Validators.maxLength(32),
        ],
      ],
      confirmPassword: [
        '',
        [Validators.required, this.matchValidator('password')],
      ],
      recaptcha: ['', Validators.required],
    });
    this.registerForm.controls.password.valueChanges.subscribe(() => {
      this.registerForm.controls.confirmPassword.updateValueAndValidity();
    });
  }

  matchValidator(matchingValue: string): ValidatorFn {
    return (control: AbstractControl) => {
      return control?.value === control?.parent?.controls[matchingValue].value
        ? null
        : { isMatching: true };
    };
  }

  register() {
    this.accountService.register(this.registerForm.value).subscribe(
      (response) => {
        this.isRegisterSuccessInfoPopupVisible = true;
      },
      (error) => {
        this.validationErrors = error;
      }
    );
  }

  cancel() {
    this.cancelRegister.emit(false);
  }

  loadBrands() {
    this.carPropertiesService.getBrands().subscribe((brands) => {
      this.brands = brands;
    });
  }

  loadModels(id: number) {
    this.carPropertiesService.getModels(id).subscribe((models) => {
      this.models = models;
    });
  }

  loadEngines(modelId: number) {
    this.carPropertiesService.getEngines(modelId).subscribe((engines) => {
      this.engines = engines;
    });
  }

  onBrandChange(e) {
    this.registerForm.controls.modelId.setValue('');
    this.registerForm.controls.engineId.setValue('');
    const brandId: number = parseInt(e.target['value']);
    this.loadModels(brandId);
  }

  onModelChange(e: any) {
    this.registerForm.controls.engineId.setValue('');
    const modelId = parseInt(e.split(':')[1]);
    //const modelId = parseInt(e);
    this.loadEngines(modelId);
  }

  // onEngineChange(e) {
  //   this.registerForm.controls.enginePower.setValue(e.target);
  //   const modelName: number = parseInt(e.target['value']);
  // }
}
