import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgxGalleryAnimation, NgxGalleryImage, NgxGalleryOptions } from '@kolkov/ngx-gallery';
import { take } from 'rxjs/operators';
import { FuelReportView } from 'src/app/models/fuelReportView';
import { Member } from 'src/app/models/member';
import { Note } from 'src/app/models/note';
import { User } from 'src/app/models/user';
import { AccountService } from 'src/app/services/account.service';
import { MembersService } from 'src/app/services/members.service';
import { NotesService } from 'src/app/services/notes.service';
import { StatisticsService } from 'src/app/services/statistics.service';

@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css']
})
export class MemberDetailComponent implements OnInit {
  member: Member;
  user: User;
  galleryOptions: NgxGalleryOptions[];
  galleryImages: NgxGalleryImage[];
  notes: Note[];
  fuelReport: FuelReportView[];

  constructor(private memberService: MembersService, private route: ActivatedRoute, private notesService: NotesService,
    private statisticsService: StatisticsService, private accountService: AccountService) { 
      this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
    }


  ngOnInit(): void {
    this.loadMember();
    this.galleryOptions = [
      {
        imagePercent: 100,
        width: '622px',
        height: '482px',
        thumbnailsColumns: 5,
        imageAnimation: NgxGalleryAnimation.Slide,
        imageSwipe: true,
        imageInfinityMove: true,
        preview: false
      }
    ]
  }


  getImages(): NgxGalleryImage[] {
    const imageUrls = [];
    for (const photo of this.member.photos) {
      imageUrls.push(
        {
          small: photo?.url,
          medium: photo?.url,
          big: photo?.url
        }
      )
    }
    return imageUrls;
  }

  loadMember() {
    this.memberService.getMember(this.route.snapshot.paramMap.get('username')).subscribe(member => {
      this.member = member;
      this.galleryImages = this.getImages();
      this.loadNotes(member.id);
      this.loadAverageConsumption(member.id);
    })
  }

  loadAverageConsumption(id: number) {
    this.statisticsService.getAverageConsumption(id).subscribe(x => {
      this.fuelReport = x;
    });
  }

  loadNotes(id: number) {
    this.notesService.getNotes(id).subscribe(notes => {
      this.notes = notes;
    })
  }

}
