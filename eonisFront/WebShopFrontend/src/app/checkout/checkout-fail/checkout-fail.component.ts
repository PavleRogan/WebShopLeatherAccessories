import { Component, OnInit } from '@angular/core';
import { IOrder } from 'src/app/shared/models/order';
import { CheckoutService } from '../checkout.service';

@Component({
  selector: 'app-checkout-fail',
  templateUrl: './checkout-fail.component.html',
  styleUrls: ['./checkout-fail.component.scss']
})
export class CheckoutFailComponent implements OnInit {

  private order!: IOrder | null;

  constructor(private checkoutService: CheckoutService){}

  ngOnInit(): void {
   
   let orderId = localStorage.getItem('orderId');
   if(orderId){
    console.log('TRYING TO DELETE ORDER: '+ orderId);
    this.checkoutService.deleteOrder(orderId).subscribe({
      next: () => {
        console.log('Order deleted successfully');
        localStorage.removeItem('orderId'); 
      },
      error: (error) => {
        console.error('Error deleting order: ', error);
      }
    });
   }
   

  }
}
