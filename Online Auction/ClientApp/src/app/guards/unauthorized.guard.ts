import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UnauthorizedGuard implements CanActivate {
  constructor(private router: Router) {
   

  }
  canActivate(
   ): boolean 
    {
      if (window.localStorage.getItem('token') == null) {
        return true;
      } else {
        this.router.navigate(['']);
        return false;
      }
  }
}
