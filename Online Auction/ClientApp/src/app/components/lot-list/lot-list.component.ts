import { Component, OnInit } from '@angular/core';
import { LotService } from 'src/app/services/lot.service';
import { Lot } from 'src/app/models/lot';
import { Observable } from 'rxjs';
import {ActivatedRoute, Router} from '@angular/router'
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-lot-list',
  templateUrl: './lot-list.component.html',
  styleUrls: ['./lot-list.component.css']
})
export class LotListComponent implements OnInit {

  public searchString: string;
  private lots: Lot[];
  private categoryId: string;
  private pathSnapshot: string;
  private isLotsByProfile: boolean;

  constructor(
    private lotService: LotService,
    private route: ActivatedRoute,
    private router: Router,
    private toastr: ToastrService) { }

  ngOnInit() {
    this.categoryId = this.route.snapshot.paramMap.get('id');
    this.isLotsByProfile = this.route.snapshot.routeConfig.path.includes('profile');    
  
    if (this.categoryId != null) 
    {
      this.lotService.getLotsByCategoryId(this.categoryId).subscribe((data: Lot[]) => {this.lots = data})
    }
    else if (this.isLotsByProfile)
    {
      this.getLotsByUser().subscribe((data: Lot[]) => {this.lots = data});
    }
    else 
    {
      this.getLots().subscribe((data: Lot[]) => {this.lots = data});
    }
  }

  getLots() : Observable<Lot[]>
  {
    return this.lotService.getLots();
  }

  getLotsByCategoryId(categoryId) : Observable<Lot[]>
  {
    return this.lotService.getLotsByCategoryId(categoryId);
  }

  getLotsByUser() : Observable<Lot[]> 
  {
    return this.lotService.getLotsByUser();
  }

  onSelectLot (id)
  {
    this.router.navigate(['/lots', id]);
  }

  onEditLot(lot: Lot)
  {
    var now = new Date().getTime();
    var start = new Date(lot.beginDate).getTime();
    console.log(lot);
    if (now - start > 0) {
      this.toastr.error('You can not edit auction after it started');
      
    }
    else{
      this.router.navigate(['/lots', lot.id,'edit']);
      }
    
     
    
    
  }

  onDeleteLot(id)
  {
    this.lotService.deleteLot(id).subscribe(
      res => {
        this.toastr.success('The lot has been deleted');
        this.ngOnInit();
      },
      err => {
        console.log(err);
      }
    )
  }



}
