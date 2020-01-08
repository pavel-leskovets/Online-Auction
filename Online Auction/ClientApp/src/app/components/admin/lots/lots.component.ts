import { Component, OnInit } from '@angular/core';
import { Lot } from 'src/app/models/lot';
import { LotService } from 'src/app/services/lot.service';
import { ToastrService } from 'ngx-toastr';
import { Router, ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-lots',
  templateUrl: './lots.component.html',
  styleUrls: ['./lots.component.css']
})
export class LotsComponent implements OnInit {

  searchString: string;
  lots: Lot[];

  constructor(
    private lotService: LotService,
    private route: ActivatedRoute,
    private router: Router,
    private toastr: ToastrService) { }

  ngOnInit() {
    this.getLots().subscribe((data: Lot[]) => { this.lots = data });
  }

  getLots(): Observable<Lot[]> {
    return this.lotService.getLots();
  }

  onSelectLot(id) {
    this.router.navigate(['/lots', id]);
  }

  onClickDelete(id) {
    this.lotService.deleteLot(id).subscribe(
      res => {
        this.toastr.success('Lot has been deleted.');
        this.ngOnInit();
      },
      err => {
        this.toastr.error(err.error);
      }
    );
  }
}
