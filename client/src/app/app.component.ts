import { HttpClient } from '@angular/common/http';
import { Component, HostListener, OnInit, ViewEncapsulation } from '@angular/core';
import { User } from './models/user';
import { AccountService } from './services/account.service';
import { loadMessages } from 'devextreme/localization'

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'Car Portal';
  users: any;
  @HostListener("window:onbeforeunload",["$event"])
    clearLocalStorage(event){
        localStorage.clear();
    }

  constructor(private accountService: AccountService) {
    loadMessages({
      "en": {
        "dxDataGrid-editingSaveRowChanges": "Zapisz",
        "dxDataGrid-editingCancelRowChanges": "Anuluj",
        "dxDataGrid-filterRowShowAllText": "Wszystkie",
        "dxDataGrid-filterRowResetOperationText": "Resetuj",
        "dxDataGrid-filterRowOperationEquals": "Równe",
        "dxDataGrid-filterRowOperationNotEquals": "Nie równe",
        "dxDataGrid-filterRowOperationLess": "Mniej niż",
        "dxDataGrid-filterRowOperationLessOrEquals": "Mniejszy lub równy",
        "dxDataGrid-filterRowOperationGreater": "Większy niż",
        "dxDataGrid-filterRowOperationGreaterOrEquals": "Większy lub równy",
        "dxDataGrid-filterRowOperationContains": "Zawiera",
        "dxDataGrid-filterRowOperationNotContains": "Nie zawiera",
        "dxDataGrid-filterRowOperationBetween": "Pomiędzy",
        "dxDataGrid-noDataText": "Brak danych",
        "dxCollectionWidget-noDataText": "Brak danych do wyświetlenia",
        "dxDataGrid-trueText": "Tak",
        "dxDataGrid-falseText": "Nie",
      }
    });
  }

  ngOnInit() {
    this.setCurrentUser();
  }

  setCurrentUser() {
    const user: User = JSON.parse(localStorage.getItem('user'));
    this.accountService.setCurrentUser(user);
  }

}
