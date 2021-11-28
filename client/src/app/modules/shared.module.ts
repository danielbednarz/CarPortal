import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ToastrModule } from 'ngx-toastr';
import { NgxGalleryModule } from '@kolkov/ngx-gallery';
import { NgxSpinnerModule } from 'ngx-spinner';
import { FileUploadModule } from 'ng2-file-upload';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { CollapseModule } from 'ngx-bootstrap/collapse';
import {
  DxDataGridModule,
  DxListModule,
  DxTextAreaModule,
  DxPopupModule, 
  DxButtonModule,
  DxValidatorModule
} from 'devextreme-angular';
import {NgxPaginationModule} from 'ngx-pagination';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    BsDropdownModule.forRoot(),
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right'
    }),
    NgxGalleryModule,
    NgxSpinnerModule,
    FileUploadModule,
    DxDataGridModule,
    DxButtonModule,
    DxPopupModule,
    PaginationModule,
    CollapseModule,
    DxListModule,
    NgxPaginationModule,
    DxTextAreaModule,
    DxValidatorModule
  ],
  exports: [
    BsDropdownModule,
    ToastrModule,
    NgxGalleryModule,
    NgxSpinnerModule,
    FileUploadModule,
    DxDataGridModule,
    DxButtonModule,
    DxPopupModule,
    PaginationModule,
    CollapseModule,
    DxListModule,
    NgxPaginationModule,
    DxTextAreaModule,
    DxValidatorModule
  ]
})
export class SharedModule { }
