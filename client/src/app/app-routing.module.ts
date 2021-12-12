import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminPanelComponent } from './admin-panel/admin-panel.component';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { TestErrorsComponent } from './errors/test-errors/test-errors.component';
import { FindOutMoreComponent } from './find-out-more/find-out-more.component';
import { AdminGuard } from './guards/admin.guard';
import { AuthGuard } from './guards/auth.guard';
import { UnsavedChangesBlockGuard } from './guards/unsaved-changes-block.guard';
import { HomeComponent } from './home/home.component';
import { MemberMessagesComponent } from './member-messages/member-messages.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { MemberEditComponent } from './members/member-edit/member-edit.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MemberStatisticsDetailComponent } from './members/member-statistics-detail/member-statistics-detail.component';
import { MemberStatisticsComponent } from './members/member-statistics/member-statistics.component';
import { MessagesComponent } from './messages/messages.component';
import { OrganizerComponent } from './organizer/organizer.component';
import { RegisterComponent } from './register/register.component';

const routes: Routes = [
  {path: '', redirectTo: '/members', pathMatch:'full'},
  // {path: '', component: HomeComponent},
  {path: 'members', component: MemberListComponent},
  {path: 'register', component: RegisterComponent},
  {path: 'info', component: FindOutMoreComponent},
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      {path: 'members/:username', component: MemberDetailComponent},
      {path: 'member/edit', component: MemberEditComponent, canDeactivate: [UnsavedChangesBlockGuard]},
      {path: 'messages', component: MessagesComponent},
      {path: 'members/:username/messages', component: MemberMessagesComponent},
      {path: 'register', component: RegisterComponent},
      {path: 'member/edit/statistics', component: MemberStatisticsComponent},
      {path: 'members/:username/statistics', component: MemberStatisticsDetailComponent},
      {path: 'admin', component: AdminPanelComponent, canActivate: [AdminGuard]},
      {path: 'organizer', component: OrganizerComponent}
    ]
  },
  {path: 'errors', component: TestErrorsComponent},
  {path: 'not-found', component: NotFoundComponent},
  {path: 'server-error', component: ServerErrorComponent},
  {path: '**', component: NotFoundComponent},
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
