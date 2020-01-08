import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import {NgbDate, NgbCalendar} from '@ng-bootstrap/ng-bootstrap';


@Component({
  selector: 'app-edit-date',
  templateUrl: './edit-date.component.html',
  styleUrls: ['./edit-date.component.css']
})
export class EditDateComponent implements OnInit {

  @Output() startDateEvent = new EventEmitter<Date>();
  @Output() endDateEvent = new EventEmitter<Date>();

  hoveredDate: NgbDate;
  fromDate: NgbDate;
  toDate: NgbDate;
  
  ngOnInit() {
    this.sendStartDate();
    this.sendEndtDate();
  }
  constructor(calendar: NgbCalendar) {
    this.fromDate = calendar.getToday();
    this.toDate = calendar.getNext(calendar.getToday(), 'd', 5);
  }
 
  sendStartDate()
  {
    var date = new Date(this.fromDate.year, this.fromDate.month - 1, this.fromDate.day);
    this.startDateEvent.emit(date);
  }

  sendEndtDate()
  {
    if (this.toDate != null) {
      var date = new Date(this.toDate.year, this.toDate.month - 1, this.toDate.day);
      this.endDateEvent.emit(date);
    }
    else{
      this.endDateEvent.emit(null);
    }
    
  }
  

  onDateSelection(date: NgbDate) {
       if (!this.fromDate && !this.toDate) {
      this.fromDate = date;
     } else if (this.fromDate && !this.toDate && date.after(this.fromDate)) {
      this.toDate = date;
    } else {
      this.toDate = null;
      this.fromDate = date;
    }
   
      this.sendStartDate()
      this.sendEndtDate()
  }

  isHovered(date: NgbDate) {
    return this.fromDate && !this.toDate && this.hoveredDate && date.after(this.fromDate) && date.before(this.hoveredDate);
  }

  isInside(date: NgbDate) {
    return date.after(this.fromDate) && date.before(this.toDate);
  }

  isRange(date: NgbDate) {
    return date.equals(this.fromDate) || date.equals(this.toDate) || this.isInside(date) || this.isHovered(date);
  }



}
