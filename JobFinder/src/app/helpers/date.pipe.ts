import { Pipe, PipeTransform } from '@angular/core';
import { DatePipe } from '@angular/common';

@Pipe({
  name: 'datecustom'
})
export class DatePipes implements PipeTransform {

  constructor(public formatter : DatePipe) {
      
  }
  transform(value: any, ...args): any {
    console.log('dupa');
    try{
      let isCurrent = args[2];
      let firstDate = this.formatter.transform(args[0], 'yyyy/MM/dd');
      let secondDate = this.formatter.transform(args[1], 'yyyy/MM/dd');
      if(isCurrent){
        return "od " + firstDate + " do dzisiaj";
      }
      return "od " + firstDate + " do " + secondDate;
    }
    catch(errors){
      return "aaasdsad"
    }    
  }

}
