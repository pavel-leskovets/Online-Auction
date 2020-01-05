import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent implements OnInit {

  constructor(
    public authService: AuthService,
    private toastr: ToastrService,
    private router: Router) { }

  ngOnInit() {
    this.authService.signUpForm.reset();
  }

  onSubmit()
  {
    this.authService.signUp().subscribe(
      res => {
          this.toastr.success('New user created', 'Registration successful');
          this.authService.signUpForm.reset();
          this.router.navigate(['/signin']);
      },
      err => {
          this.toastr.error(err.error);
        })
   }

}
