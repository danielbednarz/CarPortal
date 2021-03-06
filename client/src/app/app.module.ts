import { Component, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http'; 

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavComponent } from './nav/nav.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { MessagesComponent } from './messages/messages.component';
import { SharedModule } from './modules/shared.module';
import { TestErrorsComponent } from './errors/test-errors/test-errors.component';
import { ErrorInterceptor } from './interceptors/error.interceptor';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { MemberCardComponent } from './members/member-card/member-card.component';
import { JwtInterceptor } from './interceptors/jwt.interceptor';
import { MemberEditComponent } from './members/member-edit/member-edit.component';
import { LoadingInterceptor } from './interceptors/loading.interceptor';
import { FindOutMoreComponent } from './find-out-more/find-out-more.component';
import { DxChartModule, DxSelectBoxModule } from 'devextreme-angular';
import { PhotoAddComponent } from './members/photo-add/photo-add.component';
import { MemberStatisticsComponent } from './members/member-statistics/member-statistics.component';
import { MemberStatisticsDetailComponent } from './members/member-statistics-detail/member-statistics-detail.component';
import { NotesComponent } from './notes/notes.component';
import { MemberMessagesComponent } from './member-messages/member-messages.component';
import { AdminPanelComponent } from './admin-panel/admin-panel.component';
import { CheckRoleDirective } from './directives/check-role.directive';
import { PhotoManagementComponent } from './admin-panel/photo-management/photo-management.component';
import { UserManagementComponent } from './admin-panel/user-management/user-management.component';
import { OrganizerComponent } from './organizer/organizer.component';
import { CarInsuranceComponent } from './organizer/car-insurance/car-insurance.component';
import { PeriodicInspectionComponent } from './organizer/periodic-inspection/periodic-inspection.component';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    HomeComponent,
    RegisterComponent,
    MemberListComponent,
    MemberDetailComponent,
    MessagesComponent,
    TestErrorsComponent,
    NotFoundComponent,
    ServerErrorComponent,
    MemberCardComponent,
    MemberEditComponent,
    FindOutMoreComponent,
    PhotoAddComponent,
    MemberStatisticsComponent,
    MemberStatisticsDetailComponent,
    NotesComponent,
    MemberMessagesComponent,
    AdminPanelComponent,
    CheckRoleDirective,
    PhotoManagementComponent,
    UserManagementComponent,
    OrganizerComponent,
    CarInsuranceComponent,
    PeriodicInspectionComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule,
    DxSelectBoxModule,
    DxChartModule
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi: true}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
 