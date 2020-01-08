import { Component, OnInit } from '@angular/core';
import { LotService } from 'src/app/services/lot.service';
import { ToastrService } from 'ngx-toastr';
import { Category } from 'src/app/models/category';
import { CategoryService } from 'src/app/services/category.service';
import { Observable } from 'rxjs';
import { NgForm } from '@angular/forms'

@Component({
  selector: 'app-create-lot',
  templateUrl: './create-lot.component.html',
  styleUrls: ['./create-lot.component.css']
})
export class CreateLotComponent implements OnInit {

  imageUrl: string;
  image: File = null;
  categories: Category[];
  fromDate: Date;
  toDate: Date;

  constructor(
    public lotService: LotService,
    private toastr: ToastrService,
    private categoryService: CategoryService) { }

  ngOnInit() {
    this.imageUrl = "";
    this.lotService.AddLotForm.reset();
    this.getCategories().subscribe((data: Category[]) => this.categories = data);
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
    if (this.image == null) {
      this.toastr.error('Image required');
      return;
    }
    if (start - now < 0) {
      this.toastr.error("Auction can't begin earlier than now");
    }
    else {
      this.lotService.createLot(this.image).subscribe(
        res => {
          this.toastr.success('New lot has been created');
          this.ngOnInit();
        },
        err => {
          console.log(err);
          this.toastr.error(err.error);
        }
      )
    }
  }
}
