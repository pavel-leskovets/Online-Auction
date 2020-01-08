import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/models/user';
import { UserService } from 'src/app/services/user.service';
import { Observable } from 'rxjs';
import { AuthService } from 'src/app/services/auth.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {

  editMode: boolean;
  currentUser: User;

  constructor(
    private authService: AuthService,
    private userService: UserService,
    private toastr: ToastrService) { }

  ngOnInit() {
    this.editMode = false;
    this.authService.getCurrentUser().subscribe((data: User) => this.currentUser = data);
  }


  onEditMode()
  {
    this.editMode = true;
  }

  onSubmit()
  {
    this.userService.updateProfile(this.currentUser.id).subscribe(
      res => {
        this.toastr.success('Success', 'Profile is updated');
        this.authService.getUserProfile();
        this.ngOnInit();
      },
      err => {
        console.log(err);
      }
    )
  }


}
