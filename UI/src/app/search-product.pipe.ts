import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'searchProduct'
})
export class SearchProductPipe implements PipeTransform {
  transform(value:any,  searchtext ?:any): any {
    if(!value) return null;
    if(! searchtext) return value;

    return value.filter(function(values:any) {

      return values.name.toLowerCase().includes(searchtext.toLowerCase());

    })

  }
}