import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { IOrder } from '../shared/models/order';
import { CheckoutService } from './checkout.service';
import { IUser } from '../shared/models/user';
import { AccountService } from '../account/account.service';
import { Router } from '@angular/router';

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
    private router: Router
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
    console.log(JSON.stringify(this.order));
    this.router.navigate(['/checkout/payment']);
  }
}
