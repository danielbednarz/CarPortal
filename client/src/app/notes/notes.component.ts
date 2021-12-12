import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs/operators';
import { Member } from '../models/member';
import { Note } from '../models/note';
import { User } from '../models/user';
import { AccountService } from '../services/account.service';
import { MembersService } from '../services/members.service';
import { NotesService } from '../services/notes.service';

@Component({
  selector: 'app-notes',
  templateUrl: './notes.component.html',
  styleUrls: ['./notes.component.css']
})
export class NotesComponent implements OnInit {
  @Input() note: Note;
  user: User;
  username: string;

  constructor(private noteService: NotesService, private toastrService: ToastrService, public router: Router,
    public accountService: AccountService, private route: ActivatedRoute, private memberService: MembersService) { 
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
    }

  ngOnInit(): void {
    this.username = this.route.snapshot.paramMap.get('username');
  }

  deleteNote() {
    this.noteService.deleteNote(this.note.id).subscribe(() => {
      this.toastrService.success("Wpis został usunięty");
      this.reloadComponent();
    });
  }
  
  reloadComponent() {
    let currentUrl = this.router.url;
    this.router.routeReuseStrategy.shouldReuseRoute = () => false;
    this.router.onSameUrlNavigation = 'reload';
    this.router.navigate([currentUrl]);
 }

}
