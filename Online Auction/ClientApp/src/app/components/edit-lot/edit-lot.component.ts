import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { User } from 'src/app/models/user';
import { Lot } from 'src/app/models/lot';
import { Observable } from 'rxjs';
import { LotService } from 'src/app/services/lot.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-edit-lot',
  templateUrl: './edit-lot.component.html',
  styleUrls: ['./edit-lot.component.css']
})
export class EditLotComponent implements OnInit {
  private lotId: string;
  private currentUser: User;
  private currentLot: Lot;
  interval: any;

  constructor(
    private authService: AuthService,
    private lotService: LotService,
    private route: ActivatedRoute,
    private router: Router) { }

  ngOnInit() {
    this.lotId = this.route.snapshot.paramMap.get('id');

    this.authService.getCurrentUser().subscribe((data: User) => this.currentUser = data);

    this.getLot().subscribe((data: Lot) => this.currentLot = data);

    this.checkUser();
  }

  checkUser() {
    this.interval = setInterval(() => {
      if (this.currentLot != null && this.currentUser != null) {
        if (this.currentLot.userId != this.currentUser.id) {
          clearInterval(this.interval);
          this.router.navigateByUrl('');
        }
        else {
          clearInterval(this.interval);
        }
      }
    }, 100)
  }




  getLot(): Observable<Lot> {
    return this.lotService.getLot(this.lotId)
  }

}
