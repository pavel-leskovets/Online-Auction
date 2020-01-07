import { Component, OnInit, Injectable } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { User } from 'src/app/models/user';
import { UserService } from 'src/app/services/user.service';
import { Observable } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { LotService } from 'src/app/services/lot.service';


@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
 
  public isLoggedIn : boolean;
  public currentUser: User;

  constructor( 
    private router: Router, 
    private authService: AuthService,
    private userService: UserService,
    private toastr: ToastrService,
    private lotService: LotService) { }
  
   ngOnInit(){
     
     this.authService.isLoggedIn().subscribe((data: boolean) => this.isLoggedIn = data);
     if (this.isLoggedIn) {
      this.authService.getUserProfile();
     }
    
    this.authService.getCurrentUser().subscribe((data: User) => this.currentUser = data);
    console.log(this.currentUser);
  } 

 
  onLogout()
  {
    window.localStorage.removeItem('token');
    this.authService.isLoginSubject.next(false);
    this.authService.currentUserSubject.next(null);
    this.ngOnInit();
    this.router.navigate(['signin']);
  }

  isAdmin() : boolean
  {
    if (this.isLoggedIn) {
      return this.userService.isAdmin();
    }
    return false;
  }

  isModerator() : boolean
  {
    if (this.isLoggedIn) {
      return this.userService.isModerator();
    }
    return false;
  }

}
