import { Component, Input, OnInit } from '@angular/core';
import { Member } from 'src/app/models/member';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-member-card',
  templateUrl: './member-card.component.html',
  styleUrls: ['./member-card.component.css']
})
export class MemberCardComponent implements OnInit {
  @Input() member: Member;

  constructor(public accountService: AccountService) { }

  ngOnInit(): void {
  }

}
