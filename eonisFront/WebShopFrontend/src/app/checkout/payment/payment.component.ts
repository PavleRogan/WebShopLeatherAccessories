import { AfterViewInit, Component, ElementRef, Input, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { IOrder, Order } from 'src/app/shared/models/order';
import { CheckoutService } from '../checkout.service';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { loadStripe, Stripe, StripeCardCvcElement, StripeCardExpiryElement, StripeCardNumberElement } from '@stripe/stripe-js';

@Component({
  selector: 'app-payment',
  templateUrl: './payment.component.html',
  styleUrls: ['./payment.component.scss']
})
export class PaymentComponent implements OnInit{
  order: IOrder | null = null;

  @Input() checkoutForm?: FormGroup;
  @ViewChild('cardNumber') cardNumberElement?: ElementRef;
  @ViewChild('cardExpiry') cardExpiryElement?: ElementRef;
  @ViewChild('cardCvc') cardCvcElement?: ElementRef;
  stripe: Stripe | null = null;
  cardNumber?: StripeCardNumberElement;
  cardExpiry?: StripeCardExpiryElement;
  cardCvc?: StripeCardCvcElement;
  cardNumberComplete = false;
  cardExpiryComplete = false;
  cardCvcComplete = false;
  cardErrors: any;

  constructor(private checkoutService: CheckoutService, private router: Router, private snackBar: MatSnackBar) {}

  ngOnInit(): void {
    this.order = this.checkoutService.getOrder();
    loadStripe('pk_test_51PNAXTBmqbzHymHyBnSmZEr7w22KHB4vhHBxwi0TrUjiINkUqijQUlAjJ7GCpnTd8d4bSwFTjmn91FHHmFLBVRmp00EAi8zBYk').then(stripe => {
      this.stripe = stripe;
    });
  }

  ngAfterViewInit(): void {
    if (this.stripe) {
      const elements = this.stripe.elements();
      if (elements) {
        if (this.cardNumberElement?.nativeElement) {
          this.cardNumber = elements.create('cardNumber');
          this.cardNumber.mount(this.cardNumberElement.nativeElement);
          this.cardNumber.on('change', event => {
            this.cardNumberComplete = event.complete;
            if (event.error) this.cardErrors = event.error.message;
            else this.cardErrors = null;
          });
        }

        if (this.cardExpiryElement?.nativeElement) {
          this.cardExpiry = elements.create('cardExpiry');
          this.cardExpiry.mount(this.cardExpiryElement.nativeElement);
          this.cardExpiry.on('change', event => {
            this.cardExpiryComplete = event.complete;
            if (event.error) this.cardErrors = event.error.message;
            else this.cardErrors = null;
          });
        }

        if (this.cardCvcElement?.nativeElement) {
          this.cardCvc = elements.create('cardCvc');
          this.cardCvc.mount(this.cardCvcElement.nativeElement);
          this.cardCvc.on('change', event => {
            this.cardCvcComplete = event.complete;
            if (event.error) this.cardErrors = event.error.message;
            else this.cardErrors = null;
          });
        }
      }
    }
  }

  get paymentFormComplete() {
    return this.checkoutForm?.get('paymentForm')?.valid 
      && this.cardNumberComplete 
      && this.cardExpiryComplete 
      && this.cardCvcComplete;
  }


onSubmit() {
  if (this.order) {

    this.checkoutService.createOrder(this.order).subscribe({
      next: () => {
        console.log('Order created successfully');
        // Handle success, e.g., navigate to order confirmation page
        this.createPaymentIntent();
        
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
createPaymentIntent() {
  this.checkoutService.createPaymentIntent().subscribe((response: any) => {
    this.router.navigate(['checkout/success']); 
    this.snackBar.open('Payment success!', 'OK', {
      duration: 3000,
    });

  }, error => {
    console.log(error);
    if (this.order) {
      this.checkoutService.deleteOrder().subscribe(() => {
        this.snackBar.open('Payment error. Please try again. ' + error.message, 'Close', {
          duration: 3000,
        });
      }, deleteError => {
        console.error('Error deleting order:', deleteError);
        this.snackBar.open('Payment error. An error occurred while deleting the order.', 'Close', {
          duration: 3000,
        });
      });
    } else {
      this.snackBar.open('Payment error. An error occurred while deleting the order.', 'Close', {
        duration: 3000,
      });
    }
  });
}

}


