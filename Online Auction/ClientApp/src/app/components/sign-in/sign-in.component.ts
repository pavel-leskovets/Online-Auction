import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
 
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css']
})
export class SignInComponent implements OnInit {

  constructor(
    public authService: AuthService,
    private router: Router,
    private toastr: ToastrService,
    ) { }

  ngOnInit() {
    this.authService.sigInForm.reset();
  }

  onSubmit()
  {
    this.authService.signIn().subscribe(
      (res: any) =>{
        window.localStorage.setItem('token', res.token);
        this.authService.isLoginSubject.next(true);
        this.authService.getUserProfile();
        this.router.navigate(['']);
      },
      err => {
        if (err.status == 400)
        this.toastr.error('Incorect user name or password');
        else
        console.log(err);
      }
    )
  }
}
