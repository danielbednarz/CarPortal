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
  DxTemplateModule,
  DxChartModule,
  DxTextBoxModule
} from 'devextreme-angular';
import {NgxPaginationModule} from 'ngx-pagination';
import { AccordionModule } from 'ngx-bootstrap/accordion';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { TooltipModule } from 'ngx-bootstrap/tooltip';

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
    DxTemplateModule,
    DxChartModule,
    TooltipModule.forRoot(),
    DxTextBoxModule
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
    DxTemplateModule,
    DxChartModule,
    TooltipModule,
    DxTextBoxModule
  ]
})
export class SharedModule { }
