
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { BasketService } from './basket.service';
import { IOrder, IOrderItem, Order } from '../shared/models/order';
import { Router } from '@angular/router';
import { AccountService } from '../account/account.service';
import { CheckoutService } from '../checkout/checkout.service';

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrls: ['./basket.component.scss']
})
export class BasketComponent {

  orderItems: IOrderItem[] = [];
  errorMessage: string = '';
  @Output() localOrder = new EventEmitter<IOrder>();

  constructor(
    private router: Router,
    private accService: AccountService,
    private basketService: BasketService, 
    private checkoutService: CheckoutService
  ) {
    this.basketService.orderItems$.subscribe(items => {
      this.orderItems = items;
    });
  }



  getTotal() {
    return this.orderItems.reduce((total, item) => total + (item.price! * item.quantity!), 0);
  }

  deleteItem(productId: string) {
    this.basketService.deleteItem(productId);
  }

  updateQuantity(productId: string, quantity: number) {
    const item = this.orderItems.find(item => item.productId === productId);
    if (item) {
      item.quantity = quantity;
    }
  }

  validateCheckout() {
    this.errorMessage = '';
    for (const item of this.orderItems) {
      if (item.quantity! < 1) {
        this.errorMessage = 'Quantity cannot be less than 1.';
        return;
      }
    }
    
    let localOrder = new Order();
    localOrder.userId = this.accService.getCurrentUserValue()?.userId;
    localOrder.orderItems = this.orderItems;
    this.checkoutService.setOrder(localOrder);

    if (this.errorMessage === '') {
      console.log(this.orderItems);
      this.router.navigate(['/checkout']);
    }
  }

}

