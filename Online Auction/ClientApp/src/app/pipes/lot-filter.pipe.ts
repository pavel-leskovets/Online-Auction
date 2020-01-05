import { Pipe, PipeTransform } from '@angular/core';
import { Lot } from '../models/lot';

@Pipe({
  name: 'lotFilter'
})
export class LotFilterPipe implements PipeTransform {

  transform(lots: Lot[], search: string =''): Lot[] {
    if (!search.trim()) {
      return lots;
    }

    return lots.filter(lots => {
      return lots.name.toLowerCase().indexOf(search.toLowerCase()) !== -1;

    })
  }

}
