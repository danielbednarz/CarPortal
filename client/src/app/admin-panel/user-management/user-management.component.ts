import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/models/user';
import { AdminService } from 'src/app/services/admin.service';
import { loadMessages } from 'devextreme/localization'

@Component({
  selector: 'app-user-management',
  templateUrl: './user-management.component.html',
  styleUrls: ['./user-management.component.css']
})
export class UserManagementComponent implements OnInit {
  users: Partial<User[]>;
  roleTypes: any;
  saveButtonOptions: any;

  constructor(private adminService: AdminService) { 

  }

  ngOnInit(): void {
    this.getUsersWithRoles();
    this.roleTypes = [
      {text: 'Administrator', value: 'Admin'},
      {text: 'Użytkownik', value: 'User'}
    ];
    this.saveButtonOptions = {
      text: 'Zapisz'
    };
  }

  getUsersWithRoles() {
    this.adminService.getUsersWithRoles().subscribe(users => {
      this.users = users;
    })
  }

  updateUserRole(e) {
    const username = e.oldData.username;
    const role = e.newData.roles;

    this.adminService.updateUserRoles(username, role).subscribe(() => {
      const user = this.users.find(x => x.username === username);
      user.roles = [role];
    })
  }


}
