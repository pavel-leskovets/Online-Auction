import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Lot } from '../models/lot';
import { FormBuilder, Validators } from '@angular/forms';
import { AuthService } from './auth.service';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class LotService {

  private rootUrl = 'https://localhost:44334/api';
  currentUser: User;

  constructor(
    private http: HttpClient,
    private fb: FormBuilder,
    private authService: AuthService) { 
      authService.getCurrentUser().subscribe((data: User) => this.currentUser = data);
    }

    AddLotForm = this.fb.group({
      LotName: ['', Validators.required],
      InitialPrice: ['', [Validators.required, Validators.min(1)]],
      CategoryId: ['', Validators.required],
      BeginDate: ['', Validators.required],
      EndDate: ['', Validators.required],
      Description: ['', Validators.required],
      Image: ['', Validators.required]
    })

    createLot(image)
    {
      const formData: FormData = new FormData();

     if (this.currentUser) {
      formData.append('UserId', this.currentUser.id.toString());
     }
      formData.append('Name', this.AddLotForm.value.LotName);
      formData.append('InitialPrice', this.AddLotForm.value.InitialPrice);
      formData.append('CategoryId', this.AddLotForm.value.CategoryId);
      formData.append('BeginDate', this.AddLotForm.value.BeginDate.toISOString());
      formData.append('EndDate', this.AddLotForm.value.EndDate.toISOString());
      formData.append('Description', this.AddLotForm.value.Description);
      formData.append('Image', image, image.name);
      
      
      console.log(formData);
      return this.http.post(this.rootUrl + '/lots', formData );  
    }
 

  getLot(id): Observable<Lot> {
    return this.http.get<Lot>(this.rootUrl + '/lots/' + id);
  }

  getLots(): Observable<Lot[]> {
    return this.http.get<Lot[]>(this.rootUrl + '/lots');

  }

  getLotsByCategoryId(id) {
    return this.http.get<Lot[]>(this.rootUrl + '/categories/' + id + '/lots');
  }

  getLotsByUser() : Observable<Lot[]> {
    return this.http.get<Lot[]>(this.rootUrl + '/users/profile/lots');
  }

  updateLot(image, lotId)
  {
    const formData: FormData = new FormData();

    if (this.currentUser) {
     formData.append('UserId', this.currentUser.id.toString());
    }
     formData.append('id', lotId);
     formData.append('Name', this.AddLotForm.value.LotName);
     formData.append('InitialPrice', this.AddLotForm.value.InitialPrice);
     formData.append('CategoryId', this.AddLotForm.value.CategoryId);
     formData.append('BeginDate', this.AddLotForm.value.BeginDate.toISOString());
     formData.append('EndDate', this.AddLotForm.value.EndDate.toISOString());
     formData.append('Description', this.AddLotForm.value.Description);
     formData.append('Image', image, image.name);
     
     
     console.log(formData);
     return this.http.put(this.rootUrl + '/lots/' + lotId, formData );  
  }

  deleteLot(id)
  {
    return this.http.delete(this.rootUrl + '/lots/' + id);
  }
}