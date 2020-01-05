import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent} from '@angular/common/http'
import { Observable } from 'rxjs';
import { tap } from "rxjs/operators"
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class TokenInterceptorService implements HttpInterceptor{
  
  constructor(
    private router: Router,
    private toastr: ToastrService,
    private authService: AuthService
    ) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (window.localStorage.getItem('token') != null) {
      const clonedReq = req.clone({
        headers: req.headers.set('Authorization','Bearer ' + window.localStorage.getItem('token'))
      });
      return next.handle(clonedReq).pipe(
        tap(
          succ => {},
          err => {
            if (err.status == 401)
            {
              window.localStorage.removeItem('token');
              this.authService.isLoginSubject.next(false); 
              this.authService.currentUserSubject.next(null);
              this.router.navigateByUrl('signin');
            }
            else if (err.status == 403) {
              this.toastr.error('forebidden');
            }
            else if (err.status == 404)
            {
              this.toastr.error('Not found');
            }
          }
        )
      )
    } else {
      return next.handle(req.clone());
    }
    
    
  }
}
