import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/services/user.service';
import { User } from 'src/app/models/user';
import { Observable } from 'rxjs';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {

  users: User[];
  
  constructor(
    private userService: UserService,
    private toastr: ToastrService) { }

  ngOnInit() {
    this.getAllUsers().subscribe((data : User[]) => this.users = data);
  }

  getAllUsers() : Observable<User[]>
  {
    return this.userService.getAllUsers();
  }

  onClickDelete(id)
  {
    this.userService.deleteUser(id).subscribe(
      res => {
        this.toastr.success('User was deleted');
        this.ngOnInit();
      }
    );
  }

}
