import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { LotService } from 'src/app/services/lot.service';
import { Lot } from 'src/app/models/lot';
import { Observable } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { UserService } from 'src/app/services/user.service';
import { BidService } from 'src/app/services/bid.service';
import { Bid } from 'src/app/models/bid';
import { AuthService } from 'src/app/services/auth.service';
import { User } from 'src/app/models/user';
import { Timer } from 'src/app/models/timer';


@Component({
  selector: 'app-lot',
  templateUrl: './lot.component.html',
  styleUrls: ['./lot.component.css']
})
export class LotComponent implements OnInit {

  lotIsStarted: boolean;
  lotIsExpired: boolean;
  timer: Timer = new Timer();
  currentUser: User;
  bidPrice: number;
  lot: Lot;
  lotId: string;

  constructor(
    private route: ActivatedRoute,
    private lotService: LotService,
    private toastr: ToastrService,
    private userService: UserService,
    private bidService: BidService,
    private authService: AuthService) { }

  ngOnInit() {
    this.authService.getCurrentUser().subscribe((data: User) => this.currentUser = data);

    this.lotId = this.route.snapshot.paramMap.get('id');

    this.getLot().subscribe((data: Lot) => {
      this.lot = data;
      this.bidPrice = Math.round(this.lot.currentPrice * 1.05 + 1);
      this.timer.Countdown(new Date(data.endDate), new Date(data.beginDate));
    })

    this.timer.isExpaired().subscribe((data: boolean) => this.lotIsExpired = data);
    this.timer.isStarted().subscribe((data: boolean) => this.lotIsStarted = data)

  }

  sortedBids(): Bid[] {
    return this.lot.bids.sort((a, b) => {
      return <any>new Number(b.bidPrice) - <any>new Number(a.bidPrice);
    });
  }

  placeBid() {
    if (this.userService.getToken() == null) {
      this.toastr.error('Unauthorized user can not place bids!')
    }
    else if (this.bidPrice < this.lot.currentPrice || this.bidPrice == this.lot.currentPrice) {
      this.toastr.error('Bid can not be lower or equal the current price!')
    }
    else if (this.lot.userId == this.currentUser.id) {
      this.toastr.error('You can not place bid on own lots!')
    }
    else {
      var date = new Date();

      const body = {
        userName: this.currentUser.userName,
        bidPrice: this.bidPrice,
        userId: this.currentUser.id,
        lotId: this.lot.id,
        bidDate: new Date(date.getTime() - (date.getTimezoneOffset() * 60000)).toISOString()
      }
      console.log(body);
      this.bidService.createBid(body).subscribe(
        res => {
          this.toastr.success('Success', 'New bid has been created');
          this.ngOnInit();
        },
        err => {
          console.log(err);
          this.toastr.error(err.error);
        }
      );
    }
  }

  getLot(): Observable<Lot> {
    return this.lotService.getLot(this.lotId);
  }

}
