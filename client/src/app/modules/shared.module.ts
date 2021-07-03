import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ToastrModule } from 'ngx-toastr';
import { DxButtonModule, DxDataGridModule } from 'devextreme-angular';



@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    BsDropdownModule.forRoot(),
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right'
    }),
    DxButtonModule,
    DxDataGridModule
  ],
  exports: [
    BsDropdownModule,
    ToastrModule,
    DxButtonModule,
    DxDataGridModule
  ]
})
export class SharedModule { }
