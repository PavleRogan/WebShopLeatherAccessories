import { Injectable } from '@angular/core';
import { IOrder } from '../shared/models/order';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, catchError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CheckoutService {
  
  baseUrl = "https://localhost:7010/api/";
  private order: IOrder | null = null;

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
}

