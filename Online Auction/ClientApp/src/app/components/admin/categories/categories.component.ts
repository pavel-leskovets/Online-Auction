import { Component, OnInit } from '@angular/core';
import { CategoryService } from 'src/app/services/category.service';
import { Category } from 'src/app/models/category';
import { Observable } from 'rxjs';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.css']
})
export class CategoriesComponent implements OnInit {

  categories: Category[];
  newCategoryName: string = '';
  
  constructor(
    private categoryService: CategoryService,
    private toastr: ToastrService) { }

  ngOnInit() {
    this.getCategories().subscribe((data: Category[]) => { this.categories = data });

  }

  getCategories(): Observable<Category[]> {
    return this.categoryService.getCategories();
  }

  onClickDelete(id) {
    this.categoryService.deleteCategory(id).subscribe(
      res => {
        this.toastr.success('Category has been deleted');
        this.ngOnInit();
      }
    )
  }

  onEdit(id) {
    this.categoryService.updateCategory(id, this.categories.find(x => x.id == id).name).subscribe(
      res => {
        this.toastr.success('Category has been updated');
        this.ngOnInit();
      }
    );
  }

  onCreate() {
    if (this.newCategoryName.length == 0) {
      this.toastr.error('Category name is empty');
    }
    else {
      console.log(this.newCategoryName);
      this.categoryService.createCategory(this.newCategoryName).subscribe(
        res => {
          this.toastr.success('New category has been created');
          this.newCategoryName = '';
          this.ngOnInit();
        },
        err => {
          console.log(err)
        }
      );
    }
  }
}
