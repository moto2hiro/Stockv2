import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class StringUtils {

  constructor() { }

  toInt(input) {
    return parseInt(input);
  }

  toBool(input) {
    return (input === 1) || (input === '1') || (input === true) || (input === 'true');
  }
}
