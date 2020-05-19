import { Pipe, PipeTransform } from '@angular/core';
import { Colour } from './person..model';

@Pipe({
  name: 'colours'
})
export class ColoursPipe implements PipeTransform {

  transform(value: Colour[], ...args: unknown[]): unknown {
    return value.map(x => x.name).join(',');
  }
}