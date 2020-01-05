import { Component, OnInit } from '@angular/core';
import { BidService } from 'src/app/services/bid.service';
import { Observable } from 'rxjs';
import { Bid } from 'src/app/models/bid';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-bid-list',
  templateUrl: './bid-list.component.html',
  styleUrls: ['./bid-list.component.css']
})
export class BidListComponent implements OnInit {

  bids: Bid[];
  constructor(
    private bidService: BidService,
    private router: Router,
    private toastr: ToastrService
    ) { }

  ngOnInit( ) {
    this.getBidsByUser().subscribe((data: Bid[]) => this.bids = data);
  }

  getBidsByUser() : Observable<Bid[]>
  {
    return this.bidService.getBidsByUser()
  }

  sortedBids() : Bid[] {
    if (this.bids != null) {
      return this.bids.sort((a, b) => {
        return <any>new Date(b.bidDate) - <any>new Date(a.bidDate);
      });
    }
    
  }

  onSelect(id)
  {
    this.router.navigate(['/lots', id]);
  }
  
}
