import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { IOrder } from '../shared/models/order';
import { CheckoutService } from './checkout.service';
import { IUser } from '../shared/models/user';
import { AccountService } from '../account/account.service';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.scss']
})
export class CheckoutComponent implements OnInit {
  order: IOrder | null = null;
  currUser!: IUser | null;
  userForm!: FormGroup;

  constructor(private checkoutService: CheckoutService, private accService: AccountService, private fb: FormBuilder,
    private router: Router, private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.order = this.checkoutService.getOrder(); 
    this.currUser = this.accService.getCurrentUserValue();

    this.userForm = this.fb.group({
      name: [{value: this.currUser?.name || '', disabled: true}],
      address: [{value: this.currUser?.streetAndNumber || '', disabled: true}],
      city: [{value: this.currUser?.city || '', disabled: true}],
      postalCode: [{value: this.currUser?.postalCode || '', disabled: true}],
      contact: [{value: this.currUser?.contactNumber || '', disabled: true}]

    });
  }

  getTotalAmount(): number {
    if (!this.order || !this.order.orderItems || this.order.orderItems.length === 0) {
      return 0;
    }

    return this.order.orderItems.reduce((total, item) => total + (item.price || 0) * (item.quantity || 0), 0);
  }

  onPay(): void {
    if (this.order) {

      this.checkoutService.createOrder(this.order).subscribe({
        next: () => {
          localStorage.setItem('orderId',this.order!.orderId!);
          this.checkoutService.requestSession(this.order!.orderId!);
          
        },
        error: (error) => {
          console.error('Error creating order:', error);
          this.snackBar.open('This order is not currently available. Please try again.', 'Close', {
            duration: 3000,
          });
  
        }
      });
    } else {
      console.error('No order available to create.');
    }
  }
}
