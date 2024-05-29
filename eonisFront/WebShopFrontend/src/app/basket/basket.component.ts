import { Component, EventEmitter, Output, inject } from '@angular/core';
import { BasketService } from './basket.service';
import { IOrderItem } from '../shared/models/order';
import { Router } from '@angular/router';

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrls: ['./basket.component.scss']
})
export class BasketComponent {

  orderItems : IOrderItem[] = [];
  basketService = inject(BasketService);
  errorMessage: string = '';

constructor(private router: Router){
  this.orderItems= this.basketService.getItems();
   // Filter out duplicate items based on productId
   const uniqueItemsMap = new Map<string, IOrderItem>();
   this.orderItems.forEach(item => {
     if (!uniqueItemsMap.has(item.productId!)) {
       uniqueItemsMap.set(item.productId!, item);
     }
   });

   // Convert the map values back to an array
   this.orderItems = Array.from(uniqueItemsMap.values());
}


getTotal() {
  return this.orderItems.reduce((total, item) => total + (item.price! * item.quantity!), 0);
}

removeItem(productId: string) {
  const index = this.orderItems.findIndex(item => item.productId === productId);
  if (index > -1) {
    this.orderItems.splice(index, 1);
  }
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
  // Proceed with checkout logic
  // dodaj kreiranje porudzbine ovde bar lokalno
  if(this.errorMessage ==''){
    console.log(this.orderItems);
    this.router.navigate(['/checkout']);
  }
}
}
