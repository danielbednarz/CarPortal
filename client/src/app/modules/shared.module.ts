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
  DxValidatorModule,
  DxFormModule,
  DxNumberBoxModule,
  DxDateBoxModule,
  DxPieChartModule,
  DxTabPanelModule,
  DxTemplateModule
} from 'devextreme-angular';
import {NgxPaginationModule} from 'ngx-pagination';
import { AccordionModule } from 'ngx-bootstrap/accordion';
import { TabsModule } from 'ngx-bootstrap/tabs';

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
    DxValidatorModule,
    DxFormModule,
    DxNumberBoxModule,
    DxDateBoxModule,
    DxPieChartModule,
    AccordionModule.forRoot(),
    TabsModule.forRoot(),
    DxTabPanelModule,
    DxTemplateModule
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
    DxValidatorModule,
    DxFormModule,
    DxNumberBoxModule,
    DxDateBoxModule,
    DxPieChartModule,
    AccordionModule,
    TabsModule,
    DxTabPanelModule,
    DxTemplateModule
  ]
})
export class SharedModule { }
