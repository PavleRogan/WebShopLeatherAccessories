import { Component, EventEmitter, Output, inject } from '@angular/core';
import { BasketService } from './basket.service';
import { IOrderItem } from '../shared/models/order';

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrls: ['./basket.component.scss']
})
export class BasketComponent {

  orderItems : IOrderItem[] = [];
  basketService = inject(BasketService);

constructor(){
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
}
