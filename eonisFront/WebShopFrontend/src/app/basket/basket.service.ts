import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { IOrderItem } from '../shared/models/order';
import { IProduct } from '../shared/models/product';

@Injectable({
  providedIn: 'root'
})
export class BasketService {

  baseUrl = "https://localhost:7010/api/";
  
  private orderItemsSubject = new BehaviorSubject<IOrderItem[]>([]);
  orderItems$ = this.orderItemsSubject.asObservable();

  constructor(private http: HttpClient) { 
   
  }
  
  getItems(){
    return this.orderItemsSubject.value;
  }


  addItemToOrder(item: IProduct) {
    const currentItems = this.orderItemsSubject.value;
    const existingItem = currentItems.find(orderItem => orderItem.productId === item.productId);

    if (existingItem) {
      // Increase quantity if item already exists
      existingItem.quantity!++;
      this.orderItemsSubject.next([...currentItems]);
    } else {
      // Add new item if it doesn't exist
      const itemToAdd: IOrderItem = this.mapProdToOrderItem(item, 1);
      const updatedItems = [...currentItems, itemToAdd];
      this.orderItemsSubject.next(updatedItems);
    }
  }

  mapProdToOrderItem(item: IProduct, quantity: number): IOrderItem {
    return {
      productId: item.productId,
      quantity,
      name: item.name,
      price: item.price
    };
  }

  deleteItem(productId: string) {
    const currentItems = this.orderItemsSubject.value;
    const updatedItems = currentItems.filter(item => item.productId !== productId);
    this.orderItemsSubject.next(updatedItems);
  }

  clearBaket(){
    this.orderItemsSubject.next([]);
  }  
}
