import { Injectable, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Category } from '../models/category';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  
  private _rootUrl = 'https://localhost:44334/api';

  constructor(private http: HttpClient) { }


  getCategories() : Observable<Category[]>
  {
    return this.http.get<Category[]>(this._rootUrl + '/categories');
  }

 

  


}
