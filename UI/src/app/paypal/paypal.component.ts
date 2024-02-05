import { Component, OnInit } from '@angular/core';
import {render} from 'creditcardpayments/creditCardPayments'

@Component({
  selector: 'app-paypal',
  templateUrl: './paypal.component.html',
  styleUrls: ['./paypal.component.scss']
})
export class PaypalComponent implements OnInit{
  ngOnInit(): void {
    
    render(
      
      {
        id: "#myPayPal",
        currency : "USD",
        value: "1",
        onApprove(details) {
          alert('Transaction Successfull');
        }
      }
    );
  }

}
