import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { User } from 'src/app/models/user';
import { Lot } from 'src/app/models/lot';
import { Observable } from 'rxjs';
import { LotService } from 'src/app/services/lot.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Category } from 'src/app/models/category';
import { NgForm } from '@angular/forms';
import { CategoryService } from 'src/app/services/category.service';

@Component({
  selector: 'app-edit-lot',
  templateUrl: './edit-lot.component.html',
  styleUrls: ['./edit-lot.component.css']
})
export class EditLotComponent implements OnInit {

  lotId: string;
  currentUser: User;
  currentLot: Lot;
  interval: any;
  imageUrl: string;
  image: File = null;
  categories: Category[];
  fromDate: Date;
  toDate: Date;

  constructor(
    private authService: AuthService,
    private lotService: LotService,
    private route: ActivatedRoute,
    private router: Router,
    private toastr: ToastrService,
    private categoryService: CategoryService) { }

  ngOnInit() {

    this.lotId = this.route.snapshot.paramMap.get('id');

    this.authService.getCurrentUser().subscribe((data: User) => this.currentUser = data);

    this.categoryService.getCategories().subscribe((data: Category[]) => this.categories = data);
    this.getLot().subscribe((data: Lot) => this.currentLot = data);

    this.checkUser();
  }

  checkUser() {
    this.interval = setInterval(() => {
      if (this.currentLot != null && this.currentUser != null) {
        if (this.currentLot.userId != this.currentUser.id) {
          clearInterval(this.interval);
          this.router.navigate(['forbidden']);
          this.toastr.error('forbidden');
        }
        else {
          clearInterval(this.interval);
        }
      }
    }, 100)
  }

  getLot(): Observable<Lot> {
    return this.lotService.getLot(this.lotId)
  }


  getCurrentCategory() {
    return this.categories.find(x => x.id == this.currentLot.categoryId).name;
  }

  reciveStartDate($event) {
    this.lotService.AddLotForm.controls['BeginDate'].setValue($event);
    this.fromDate = $event;
  }
  reciveEndDate($event) {
    this.lotService.AddLotForm.controls['EndDate'].setValue($event);
    this.toDate = $event;
  }

  getCategories(): Observable<Category[]> {
    return this.categoryService.getCategories();
  }

  imageUpload(event) {
    var file = event.dataTransfer ? event.dataTransfer.files[0] : event.target.files[0];
    var pattern = /image-*/;
    if (!file.type.match(pattern)) {
      this.toastr.error("Invalid image format.")
    }
    else {
      this.image = (event.target as HTMLInputElement).files[0];
      this.lotService.AddLotForm.controls['Image'].setValue(this.image);
      this.currentLot.imageUrl = null;
      var reader = new FileReader();
      reader.onload = (event: any) => {
        this.imageUrl = event.target.result;
      }
      reader.readAsDataURL(file);
    }
  }

  onSubmit() {
    var now = new Date().getTime();
    var start = this.fromDate.getTime();

    if (now - start > 0) {
      this.toastr.error("Auction can't begin earlier than now");
    }
    else {
      this.lotService.updateLot(this.image, this.currentLot.id, this.currentLot.imageUrl).subscribe(
        res => {
          this.toastr.success('Lot has been successfully updated');

        },
        err => {
          console.log(err);
          this.toastr.error(err.error);
        }
      )
    }


  }
}
