import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Bid } from '../models/bid';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BidService {

  rootUrl = 'https://localhost:44334/api';

  constructor(
    private http: HttpClient) { }

  createBid(body) {
    console.log(body);
    return this.http.post(this.rootUrl + '/bids', body);
  }

  getBidsByUser(): Observable<Bid[]> {
    return this.http.get<Bid[]>(this.rootUrl + '/users/profile/bids');
  }

  deleteBid(id) {
    return this.http.delete(this.rootUrl + '/bids/' + id);
  }

  getAllBids() {
    return this.http.get<Bid[]>(this.rootUrl + '/bids');
  }
}

