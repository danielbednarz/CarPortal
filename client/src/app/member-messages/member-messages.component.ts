import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { DxTextAreaComponent } from 'devextreme-angular';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs/operators';
import { Message } from '../models/message';
import { User } from '../models/user';
import { AccountService } from '../services/account.service';
import { MessageService } from '../services/message.service';

@Component({
  selector: 'app-member-messages',
  templateUrl: './member-messages.component.html',
  styleUrls: ['./member-messages.component.css']
})
export class MemberMessagesComponent implements OnInit {
  @ViewChild('messageForm') messageForm: NgForm;
  @ViewChild(DxTextAreaComponent, {static: false}) textArea: DxTextAreaComponent;
  messages: Message[];
  username: string;
  messageContent: string;

  constructor(private messageService: MessageService, private route: ActivatedRoute, private router: Router, private toastr: ToastrService) { 
  }

  ngOnInit(): void {
    this.username = this.route.snapshot.paramMap.get('username');
    this.loadMessages();
  }

  loadMessages() {
    this.messageService.getMessageThread(this.username).subscribe(messages => {
      this.messages = messages;
    })
  }

  sendMessage() {
    if(this.messageContent) {
      this.messageService.sendMessage(this.username, this.messageContent).subscribe(message => {
          this.messages.push(message);
          this.textArea.instance.reset();
          this.toastr.success('Wiadomość wysłana pomyślnie');
          this.reloadComponent();
      })
    } else {
      this.toastr.error("Wiadomość nie może być pusta");
    }
  }

  reloadComponent() {
    let currentUrl = this.router.url;
    this.router.routeReuseStrategy.shouldReuseRoute = () => false;
    this.router.onSameUrlNavigation = 'reload';
    this.router.navigate([currentUrl]);
 }

}
