import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../models/user';
import { FormBuilder, Validators } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  
  private rootUrl = 'https://localhost:44334/api';
  constructor(
    private http: HttpClient,
    private fb: FormBuilder) { }

    UserForm = this.fb.group({
      UserName: [''],
      Emai: ['', Validators.email],
      FirstName: [''],
      LastName: [''],
      Addres: [''],
      Phone: ['', [Validators.minLength(5), Validators.maxLength(5), Validators.pattern('[0-9]+')]]
    })

    getToken() : string
    {
      return window.localStorage.getItem('token');
    }

    updateProfile(id)
    {
      var body = {
        Id: id,
        UserName: this.UserForm.value.UserName,
        Email: this.UserForm.value.Emai,
        FirstName: this.UserForm.value.FirstName,
        LastName: this.UserForm.value.LastName,
        Addres: this.UserForm.value.Addres,
        Phone: this.UserForm.value.Phone
     }
     console.log(body);
      return this.http.put(this.rootUrl + '/users/' + id, body);
    }

    getUserById(id) : Observable<User>
    {
      return this.http.get<User>(this.rootUrl + '/users/' + id);
    }
  
    getAllUsers() : Observable<User[]>
    {
      return this.http.get<User[]>(this.rootUrl + '/users');
    }

    deleteUser(id)
    {
      return this.http.delete(this.rootUrl + '/users/' + id);
    }

 

  roleMatch(allowedRoles): boolean {
    var isMatch = false;
    var payLoad = JSON.parse(window.atob(this.getToken().split('.')[1]));
    var userRole = payLoad.role;
    allowedRoles.forEach(element => {
      if (userRole == element)
        isMatch = true;
    });
    return isMatch;
  }

  
}
