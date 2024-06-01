import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IOrder } from 'src/app/shared/models/order';
import { AccountService } from '../account.service';

@Component({
  selector: 'app-my-orders',
  templateUrl: './my-orders.component.html',
  styleUrls: ['./my-orders.component.scss']
})
export class MyOrdersComponent implements OnInit {
  orders: IOrder[] = [];

  constructor(private http: HttpClient, private accService: AccountService) {}

  ngOnInit(): void {
    this.fetchUserOrders();
  }

  fetchUserOrders(): void {
    this.accService.getUserOrders().subscribe({
      next: (orders) => {
        this.orders = orders;
      },
      error: (error) => {
        console.error('Error fetching user orders:', error);
      }
    });
  }
}
