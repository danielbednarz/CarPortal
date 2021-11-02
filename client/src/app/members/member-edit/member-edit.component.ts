import { Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { NgxGalleryAnimation, NgxGalleryImage, NgxGalleryOptions } from '@kolkov/ngx-gallery';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs/operators';
import { Brand } from 'src/app/models/brand';
import { Member } from 'src/app/models/member';
import { User } from 'src/app/models/user';
import { AccountService } from 'src/app/services/account.service';
import { MembersService } from 'src/app/services/members.service';
import { CarPropertiesService } from 'src/app/services/carProperties.service';
import { Model } from 'src/app/models/model';
import value from 'globalize';
import { Engine } from 'src/app/models/engine';


@Component({
  selector: 'app-member-edit',
  templateUrl: './member-edit.component.html',
  styleUrls: ['./member-edit.component.css']
})
export class MemberEditComponent implements OnInit {
  @ViewChild('editForm') editForm: NgForm;
  member: Member;
  user: User;
  brands: Brand[];
  models: Model[];
  engines: Engine[];
  galleryOptions: NgxGalleryOptions[];
  galleryImages: NgxGalleryImage[];
  @HostListener("window:beforeunload", ['$event']) unloadNotification($event: any) {
    if(this.editForm.dirty) {
      $event.returnValue = true;
    }
  }

  constructor(private accountService: AccountService, private memberService: MembersService, 
    private toastrService: ToastrService, private router: Router, private carPropertiesService: CarPropertiesService) { 
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
  }

  ngOnInit(): void {
    this.loadMember();
    this.loadBrands();
    this.galleryOptions = [
      {
        imagePercent: 100,
        width: '622px',
        height: '482px',
        thumbnailsColumns: 4,
        imageAnimation: NgxGalleryAnimation.Slide
      }
    ]
  }

  loadBrands() {
    this.carPropertiesService.getBrands().subscribe(brands => {
      this.brands = brands;
    })
  }

  loadModels(id: number) {
    this.carPropertiesService.getModels(id).subscribe(models => {
      this.models = models;
    })
  }

  loadEngines(id: number) {
    this.carPropertiesService.getEngines(id).subscribe(engines => {
      this.engines = engines;
    })
  }

  loadMember() {
    this.memberService.getMember(this.user.username).subscribe(member => {
      this.member = member;
      this.galleryImages = this.getImages();
      this.loadModels(member.brandId);
      this.loadEngines(member.engineId);
    })
  }

  updateMember() {
    this.member.brandId = Number(this.member.brandId);
    this.member.modelId = Number(this.member.modelId);
    this.memberService.updateMember(this.member).subscribe(() => {
      this.toastrService.success("Profil został zaktualizowany");
      this.editForm.reset(this.member);
    });
    this.router.navigate([`/members/${this.member.username}`])

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

  onChange(e) {
    const brandName: number = parseInt(e.target['value']);
    this.loadModels(brandName);
  }
}


