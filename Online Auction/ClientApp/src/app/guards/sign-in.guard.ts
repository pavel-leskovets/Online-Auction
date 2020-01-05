import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { UserService } from '../services/user.service';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class SignInGuard implements CanActivate {
  
  constructor(
    private router: Router,
    private userService: UserService,
    private toastr: ToastrService
    ) {
   

  }
  canActivate( next: ActivatedRouteSnapshot
   ): boolean 
    {
      if (localStorage.getItem('token') != null) {
        let roles = next.data['permittedRoles'] as Array<string>;
        if (roles) {
          if (this.userService.roleMatch(roles)) 
            return true;
          else {
            this.router.navigate(['/forbidden']);
            this.toastr.error('Forbidden');
            return false;
          }  
          
        }

        return true;
      } else {
        
        this.router.navigate(['signin']);
        return false;
      }
  }
  
}
