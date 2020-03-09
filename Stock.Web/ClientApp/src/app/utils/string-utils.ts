import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class StringUtils {

  constructor() { }

  toInt(input) {
    return (input) ? parseInt(input) : 0;
  }

  toBool(input) {
    return (input === 1) || (input === '1') || (input === true) || (input === 'true');
  }
}
