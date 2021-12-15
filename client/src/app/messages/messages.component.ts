import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Brand } from '../models/brand';
import { Message } from '../models/message';
import { Model } from '../models/model';
import { Pagination } from '../models/paginaton';
import { UserParameters } from '../models/userParameters';
import { CarPropertiesService } from '../services/carProperties.service';
import { MessageService } from '../services/message.service';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.css']
})
export class MessagesComponent implements OnInit {
  messages: Message[] = [];
  brands: Brand[];
  models: Model[]; 
  pagination: Pagination;
  container = 'Inbox';
  pageNumber = 1;
  pageSize = 10;

  constructor(private messageService: MessageService, private carPropertiesService: CarPropertiesService, private router: Router, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.loadBrands();
    this.loadMessages();
  }

  loadUnreadMessages() {
    this.container = 'Unread';
    this.loadMessages();
  }

  loadInboxMessages() {
    this.container = 'Inbox';
    this.loadMessages();
  }

  loadOutboxMessages() {
    this.container = 'Outbox';
    this.loadMessages();
  }

  loadMessages() {
    this.messageService.getMessages(this.pageNumber, this.pageSize, this.container).subscribe(response => {
      this.messages = response.result;
      this.pagination = response.pagination;
    })
  }

  deleteMessage(messageId: string) {
    this.messageService.deleteMessage(messageId).subscribe(() => {
      this.messages.splice(this.messages.findIndex(x => x.id === messageId), 1);
      this.toastr.success("Wiadomość usunięta pomyślnie");
      this.reloadComponent();
    })
  }

  reloadComponent() {
    let currentUrl = this.router.url;
    this.router.routeReuseStrategy.shouldReuseRoute = () => false;
    this.router.onSameUrlNavigation = 'reload';
    this.router.navigate([currentUrl]);
 }

  pageChanged(event: any) {
    if(this.pageNumber !== event.page) {
      this.pageNumber = event.page;
      this.loadMessages();
    }
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

}
