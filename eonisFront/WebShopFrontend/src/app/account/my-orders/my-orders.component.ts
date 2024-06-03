import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IOrder } from 'src/app/shared/models/order';
import { AccountService } from '../account.service';
import { Observable } from 'rxjs';
import { IUser } from 'src/app/shared/models/user';

@Component({
  selector: 'app-my-orders',
  templateUrl: './my-orders.component.html',
  styleUrls: ['./my-orders.component.scss']
})
export class MyOrdersComponent implements OnInit {
  orders: IOrder[] = [];
  currentUser$!: Observable<IUser | null>;  
  currentUser!: IUser | null;

  constructor(private http: HttpClient, private accService: AccountService) {}

  ngOnInit(): void {
    this.currentUser$ = this.accService.currentUser$;
    this.currentUser$.subscribe(user => {
      this.currentUser = user;
      this.fetchUserOrders(user!.userId!);  
    });
     
  }

  fetchUserOrders(userId:string): void {
    this.accService.getUserOrders(userId).subscribe({
      next: (orders) => {
        this.orders = orders.sort((a, b) => new Date(b.orderDate!).getTime() - new Date(a.orderDate!).getTime());
      },
      error: (error) => {
        console.error('Error fetching user orders:', error);
      }
    });
  }
}
