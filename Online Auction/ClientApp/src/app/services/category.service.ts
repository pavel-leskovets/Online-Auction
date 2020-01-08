import { Injectable, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Category } from '../models/category';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  rootUrl = 'https://localhost:44334/api';

  constructor(private http: HttpClient) { }

  getCategories(): Observable<Category[]> {
    return this.http.get<Category[]>(this.rootUrl + '/categories');
  }

  deleteCategory(id) {
    return this.http.delete(this.rootUrl + '/categories/' + id)
  }

  updateCategory(id, name) {
    var body =
    {
      Id: id,
      Name: name
    }
    console.log(body);
    return this.http.put(this.rootUrl + '/categories/' + id, body)
  }

  createCategory(categoryName) {
    var body = {
      Name: categoryName
    }
    return this.http.post(this.rootUrl + '/categories', body);
  }
}
