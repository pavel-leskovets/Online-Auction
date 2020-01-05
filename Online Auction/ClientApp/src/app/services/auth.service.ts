import { Injectable, EventEmitter } from '@angular/core';
import { FormBuilder, Validators, FormGroup, FormControl } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Observable, Subscription, BehaviorSubject } from 'rxjs';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
   
  
  isLoginSubject = new BehaviorSubject<boolean>(this.hasToken());
  currentUserSubject = new BehaviorSubject<User>(null);
  
  private rootUrl = 'https://localhost:44334/api';

  constructor(private fb: FormBuilder, private http: HttpClient) {
   
   }
  
  private hasToken() : boolean {
    return !!localStorage.getItem('token');
  }

  sigInForm = this.fb.group({
    UserName: ['', Validators.required],
    Password: ['', [Validators.required, Validators.minLength(4)]],
  });

  signUpForm = this.fb.group({
    UserName: ['', Validators.required],
    Email: ['', [Validators.email, Validators.required]],
    Passwords: this.fb.group({
      Password: ['', [Validators.required, Validators.minLength(4)]],
      ConfirmPassword: ['', Validators.required]},
      {validator : this.comparePasswords}
  )
  });


  getUserProfile()
  {
    this.http.get<User>(this.rootUrl + '/users/profile').subscribe((data: User) => this.currentUserSubject.next(data));
  }

 
  getCurrentUser() : Observable<User>
  {
    return this.currentUserSubject.asObservable();
  }


  isLoggedIn() : Observable<boolean> {
    return this.isLoginSubject.asObservable();
  }


  comparePasswords(fb: FormGroup)
  {
    let checkConfirmPassword = fb.get('ConfirmPassword');
   
    if (checkConfirmPassword.errors == null || 'passwordMismatch' in checkConfirmPassword.errors) 
    {
      if (fb.get('Password').value != checkConfirmPassword.value) 
        checkConfirmPassword.setErrors({passwordMismatch: true});
      else 
        checkConfirmPassword.setErrors(null);
    } 
  }

  signIn()
  {
    var body = 
    {
      UserName: this.sigInForm.value.UserName,
      Password: this.sigInForm.value.Password
    }
    return this.http.post(this.rootUrl + '/users/login', body);
  }

  signUp() 
  {
    var body = 
    {
      UserName: this.signUpForm.value.UserName,
      Email: this.signUpForm.value.Email,
      Password: this.signUpForm.value.Passwords.Password
    }
    return this.http.post(this.rootUrl + '/users/Register', body);
  }

  

  
}
