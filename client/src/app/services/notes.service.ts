import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Note } from '../models/note';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class NotesService {
  baseUrl = environment.baseApiUrl + 'notes';
  private currentUserSource = new ReplaySubject<User>(1);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient) { }

  getNotes(userId: number) {
    return this.http.get<Note[]>(this.baseUrl + '/getNotes/' + userId);
  }

  addNote(model: any) {
    return this.http.post(this.baseUrl + '/add-note', model).pipe(
      map((user: User) => {
        if(user) {
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUserSource.next(user);
        }
      })
    );
  }

  deleteNote(noteId: string) {
    return this.http.delete(this.baseUrl + '/delete-note/' + noteId);
  }
}
