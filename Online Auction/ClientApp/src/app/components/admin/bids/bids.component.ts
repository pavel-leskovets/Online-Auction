import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Bid } from 'src/app/models/bid';
import { BidService } from 'src/app/services/bid.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-bids',
  templateUrl: './bids.component.html',
  styleUrls: ['./bids.component.css']
})
export class BidsComponent implements OnInit {

  bids: Bid[];
  constructor(
    private bidService: BidService,
    private router: Router,
    private toastr: ToastrService
    ) { }

  ngOnInit( ) {
    this.getAllBids().subscribe((data: Bid[]) => this.bids = data);
  }

  getAllBids() : Observable<Bid[]>
  {
    return this.bidService.getAllBids()
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

  onClickDelete(id)
  {
    this.bidService.deleteBid(id).subscribe(
      res => { 
        this.toastr.success('Bid has been deleted');
        this.ngOnInit();
      }
    )
  }

}
