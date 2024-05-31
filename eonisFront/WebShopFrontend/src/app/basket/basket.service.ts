import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { IOrder, IOrderItem, Order } from '../shared/models/order';
import { IProduct } from '../shared/models/product';

@Injectable({
  providedIn: 'root'
})
export class BasketService {

  baseUrl = "https://localhost:7010/api/";
  
  private orderItems : IOrderItem[] = [];

  constructor(private http: HttpClient) { 
   
  }
  getItems(){
    return this.orderItems;
  }

  addItemToOrder(item: IProduct){
    const itemToAdd: IOrderItem = this.mapProdToOrderItem(item, 1);
    this.orderItems.push(itemToAdd);
  }

  mapProdToOrderItem(item: IProduct, quantity: number): IOrderItem {
    return {
      productId: item.productId,
      quantity,
      name: item.name,
      price: item.price
    };
  }

  clearBaket(){
    this.orderItems = [];
  }
  
}
