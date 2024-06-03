import { Injectable } from '@angular/core';
import { IOrder } from '../shared/models/order';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, catchError, map, tap } from 'rxjs';
import { ISession } from '../shared/models/session';

declare const Stripe: any;

@Injectable({
  providedIn: 'root'
})
export class CheckoutService {
  
  
  baseUrl = "https://localhost:7010/api/";
  private order!: IOrder;

  constructor(private http: HttpClient) { }

  setOrder(order: IOrder) {
    this.order = order;
  }

  getOrder(): IOrder | null {
    return this.order;
  }

  createOrder(order: IOrder): Observable<any> {
    
    const token = localStorage.getItem('token');
    if (!token) {
      throw new Error('No token available.');
    }

    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);

    return this.http.post(`${this.baseUrl}orders`, order, { headers }).pipe(
      catchError((error: any) => {
        throw new Error('Error creating order: ' + error.message);
      })
    );
  }
  

  requestSession(orderId:string){
    this.http.post<ISession>(this.baseUrl + 'create-checkout-session/' + orderId,{}).subscribe((session)=>{
      this.redirectToCheckout(session.sessionId);
    });
  }


  redirectToCheckout(sessionId: string) {
    const stripe = Stripe('pk_test_51PNAXTBmqbzHymHyBnSmZEr7w22KHB4vhHBxwi0TrUjiINkUqijQUlAjJ7GCpnTd8d4bSwFTjmn91FHHmFLBVRmp00EAi8zBYk');
    stripe.redirectToCheckout({
      sessionId: sessionId
    });
  }


  deleteOrder(orderId: string) {
    const token = localStorage.getItem('token');
    if (!token) {
      throw new Error('No token available.');
    }
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);

    return this.http.delete(`${this.baseUrl}orders/${orderId}`, { headers }).pipe(
      catchError((error: any) => {
        throw new Error('Error deleting order: ' + error.message);
      })
    );
  }
}

