import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CarousselAndSidebarVisibilityService {

  constructor() { }
  private isElementVisibleSubject = new BehaviorSubject<boolean>(true);
  isElementVisible$ = this.isElementVisibleSubject.asObservable();

  hideElement() {
    this.isElementVisibleSubject.next(false);
  }
}
