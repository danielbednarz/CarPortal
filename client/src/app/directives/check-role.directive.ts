import { Directive, Input, OnInit, TemplateRef, ViewContainerRef } from '@angular/core';
import { Router } from '@angular/router';
import { take } from 'rxjs/operators';
import { User } from '../models/user';
import { AccountService } from '../services/account.service';

@Directive({
  selector: '[appCheckRole]'
})
export class CheckRoleDirective implements OnInit{
  @Input() appCheckRole: string[];
  user: User;

  constructor(private viewContainerRef: ViewContainerRef, private templateRef: TemplateRef<any>, 
    private accountService: AccountService) { 
      this.accountService.currentUser$.pipe(take(1)).subscribe(user => {
        this.user = user;
      })
    }

    ngOnInit() {
      if(!this.user?.roles || this.user == null) {
        this.viewContainerRef.clear();
        return;
      }

      if (this.user?.roles.some(x => this.appCheckRole.includes(x))) {
        this.viewContainerRef.createEmbeddedView(this.templateRef);
      }
      else {
        this.viewContainerRef.clear();
      }
    }
}
