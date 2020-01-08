import { BehaviorSubject, Observable } from 'rxjs';

export class Timer {
    begin = new BehaviorSubject<boolean>(false);
    expaired = new BehaviorSubject<boolean>(true);
    days: number;
    hours: number;
    minutes: number;
    seconds: number;
    interval: any;


    public isStarted() : Observable<boolean>
    {
      return this.begin.asObservable();
    }

    public isExpaired() : Observable<boolean>
    {
        return this.expaired.asObservable();
    }

    public Countdown(countDownDate, beginDate){
      
      this.interval = setInterval(() => {
          
      var now = new Date().getTime();
      
      var distanceToBegin = beginDate - now;
            
      var distanceToEnd = countDownDate - now;
        
      this.days = Math.floor(distanceToEnd / (1000 * 60 * 60 * 24));
      this.hours = Math.floor((distanceToEnd % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
      this.minutes = Math.floor((distanceToEnd % (1000 * 60 * 60)) / (1000 * 60));
      this.seconds = Math.floor((distanceToEnd % (1000 * 60)) / 1000);
       
      if (distanceToBegin < 0) {
        this.begin.next(true)
      }
      
      if (distanceToEnd < 0) {
        clearInterval(this.interval);
        this.expaired.next(true);
      }
      else{
        this.expaired.next(false);
      }
        }, 500)
      }
}
