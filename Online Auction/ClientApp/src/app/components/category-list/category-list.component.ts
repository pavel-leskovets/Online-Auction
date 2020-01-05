import { Component, OnInit } from '@angular/core';
import { CategoryService } from 'src/app/services/category.service';
import { Category } from 'src/app/models/category';
import { Observable } from 'rxjs';
import { Router} from '@angular/router'

@Component({
  selector: 'app-category-list',
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.css']
})
export class CategoryListComponent implements OnInit {

  categories: Category[];
  constructor (
    private categoryService: CategoryService, 
    private router: Router ) { }

  ngOnInit() {
    this.getCategories().subscribe((data: Category[]) => {this.categories = data});

  }

  getCategories() : Observable<Category[]>
  {
    return this.categoryService.getCategories();
  }

  onSelect (id)
  {
    this.router.navigate(['/categories', id, 'lots']);
  }

}
