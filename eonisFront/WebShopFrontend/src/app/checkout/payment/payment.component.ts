import { Component, OnInit } from '@angular/core';
import { IOrder, Order } from 'src/app/shared/models/order';
import { CheckoutService } from '../checkout.service';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-payment',
  templateUrl: './payment.component.html',
  styleUrls: ['./payment.component.scss']
})
export class PaymentComponent implements OnInit{

  order: IOrder | null = null;

  constructor(private checkoutService: CheckoutService, private router: Router, private snackBar: MatSnackBar){}

  ngOnInit(): void {
    this.order = this.checkoutService.getOrder(); 
    console.log(JSON.stringify(this.order))
  }

onSubmit() {
  if (this.order) {

    console.log("order creating" + JSON.stringify(this.order))

    this.checkoutService.createOrder(this.order).subscribe({
      next: () => {
        console.log('Order created successfully');
        // Handle success, e.g., navigate to order confirmation page
        this.router.navigate(['checkout/success']);
      },
      error: (error) => {
        console.error('Error creating order:', error);
        // Handle error, e.g., display error message to user
        this.snackBar.open('This order is not currently available. Please try again.', 'Close', {
          duration: 3000,
        });

      }
    });
  } else {
    console.error('No order available to create.');
    // Handle case where there is no order available
  }
}
}


