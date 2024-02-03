import { Component, OnDestroy } from '@angular/core';
import { NgbCarouselModule } from '@ng-bootstrap/ng-bootstrap';
import { CarousselAndSidebarVisibilityService } from 'src/caroussel-and-sidebar-visibility.service';
import { Subscription } from 'rxjs';
@Component({
  selector: 'app-caroussel',
  templateUrl: './caroussel.component.html',
  styleUrls: ['./caroussel.component.scss']
})
export class CarousselComponent implements OnDestroy{
  
  
  isElementVisible: boolean = true;

  private subscription: Subscription;

  constructor(private elementVisibilityService: CarousselAndSidebarVisibilityService) {
    this.subscription = this.elementVisibilityService.isElementVisible$.subscribe(
      visibility => this.isElementVisible = visibility
    );
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
  


}