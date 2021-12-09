import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  baseUrl = environment.baseApiUrl;

  constructor(private http: HttpClient) { }

  getUsersWithRoles() {
    // zwrócenie tylko niektórych prop z Usera, stąd Partial
    return this.http.get<Partial<User[]>>(this.baseUrl + 'admin/usersWithRoles');
  }

  updateUserRoles(username: string, role: string) {
    return this.http.post(this.baseUrl + 'admin/editRoles/' + username + '?roles=' + role, {});
  }

}
