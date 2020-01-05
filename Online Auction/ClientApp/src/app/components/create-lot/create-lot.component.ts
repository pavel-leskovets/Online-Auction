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

  image: File = null;
  categories: Category[];
  categoryID: NgForm;
  fromDate: Date;
  toDate: Date;

  constructor(
    public lotService: LotService,
    private toastr: ToastrService,
    private categoryService: CategoryService) { }

  ngOnInit() {
    this.lotService.AddLotForm.reset();
    this.getCategories().subscribe((data: Category[]) => this.categories = data);
  }

  
  setCategory()
  {
    this.lotService.AddLotForm.controls['BeginDate'].setValue(this.categoryID);
  }
 

  reciveStartDate($event)
  {
    this.lotService.AddLotForm.controls['BeginDate'].setValue($event);
    this.fromDate = $event;
  }
  reciveEndDate($event)
  {
    this.lotService.AddLotForm.controls['EndDate'].setValue($event);
    this.toDate = $event;

  }

  getCategories() : Observable<Category[]>
  {
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
    }
   }

  

  
  
  onSubmit() 
  {
    this.lotService.createLot(this.image).subscribe(
      res => {
        this.toastr.success('Success', 'New lot has been created');
        this.ngOnInit();
      },
      err => {
        console.log(err);
        
        this.toastr.error(err.error);
      }
    )

  }


}
