import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BsDropdownConfig } from 'ngx-bootstrap/dropdown';
import { ToastrModule, ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { Member } from '../models/member';
import { User } from '../models/user';
import { AccountService } from '../services/account.service';
import { MembersService } from '../services/members.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css'],
  providers: [{ provide: BsDropdownConfig, useValue: { isAnimated: true, autoClose: true } }]
})
export class NavComponent implements OnInit {
  @ViewChild('loginForm') loginForm: NgForm;
  model: any = {}

  constructor(public accountService: AccountService, private router: Router, private toastr: ToastrService) { }
  user: User;
  registerMode = false;

  ngOnInit(): void {
  }

  login() {
    this.accountService.login(this.model).subscribe(response => {
      this.router.navigateByUrl('/members');
      this.toastr.success('Zalogowano pomyślnie');
      this.loginForm.reset();
    }, error => {
      this.toastr.error(error.error);
    }
    )
  }

  logout() {
    this.accountService.logout();
    this.toastr.info('Wylogowano pomyślnie');
    this.router.navigateByUrl('/');
  }

  registerToggle() {
    this.registerMode = !this.registerMode;
  }

  cancelRegisterMode(event: boolean) {
    this.registerMode = event;
  }

}
