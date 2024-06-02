import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CheckoutComponent } from './checkout.component';
import { CheckoutRoutingModule } from './checkout-routing.module';
import { ReactiveFormsModule } from '@angular/forms';
import { PaymentComponent } from './payment/payment.component';
import { CheckoutSuccessComponent } from './checkout-success/checkout-success.component';

import { FormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    CheckoutComponent,
    PaymentComponent,
    CheckoutSuccessComponent
  ],
  imports: [
    CommonModule,
    CheckoutRoutingModule,
    ReactiveFormsModule,
    FormsModule
  ]
})
export class CheckoutModule { }
